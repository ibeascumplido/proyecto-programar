using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararAlumnos : MonoBehaviour {


	public List<GameObject> alumnosEnRango;

	// almacenamos cuando disparamos el ultimo proyectil, asi como el tipo, daño etc
	private float ultimoDisparo;
	private ProfesorDatos profesorDatos;


	// eliminamos al enemigo de la lista de alumnos en rango de disparo
	void alumnoDestruido(GameObject enemigo)
	{
		alumnosEnRango.Remove(enemigo);
	}

	void OnTriggerEnter2D (Collider2D otro)
	{
		if (otro.gameObject.tag.Equals("Enemigo"))
		{
			// incluimos al enemigo a la lista de alumnos en rango
			alumnosEnRango.Add(otro.gameObject);

			// añadimos enemigoDestruido al delegate para notificar cuando ha sido destruido
			AlumnoDestruidoDelegate del = otro.gameObject.GetComponent<AlumnoDestruidoDelegate>();
			del.alumnoDelegate += alumnoDestruido;
		}
	}
	// borramos al enemigo de la lista y del delegate
	void OnTriggerExit2D (Collider2D otro)
	{
		if (otro.gameObject.tag.Equals("Enemigo"))
		{
			alumnosEnRango.Remove(otro.gameObject);
			AlumnoDestruidoDelegate del = otro.gameObject.GetComponent<AlumnoDestruidoDelegate>();
			del.alumnoDelegate -= alumnoDestruido;
		}
	}



	void Disparo(Collider2D objetivo)
	{
		GameObject notaPrefab = profesorDatos.NivelActual.nota;

		// obtenemos la posicion inicial del proyectil y de su objetivo
		Vector3 posicionInicio = gameObject.transform.position;
		Vector3 posicionObjetivo = objetivo.transform.position;

		// establecemos la posicion Z del proyectil para que aparezca detras del sprite del profesor y delante del alumno
		posicionInicio.z = notaPrefab.transform.position.z;
		posicionObjetivo.z = notaPrefab.transform.position.z;

		// instanciamos una nueva nota a partir del prefab
		GameObject nuevaNota = (GameObject)Instantiate (notaPrefab);
		nuevaNota.transform.position = posicionInicio;
		NotasManejo notaComp = nuevaNota.GetComponent<NotasManejo>();
		notaComp.objetivo = objetivo.gameObject;

		// asignamos la posicion de la nota
		notaComp.posicionInicio = posicionInicio;
		notaComp.posicionObjetivo = posicionObjetivo;

		// reproducimos la animacion y el sonido de disparo
		Animator animator = profesorDatos.NivelActual.vista.GetComponent<Animator>();
		animator.SetTrigger("disparar");
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.PlayOneShot(audioSource.clip);
	}




	void Start () {

		// creamos la lista donde iremos incluyendo los alumnos que entran en rango
		alumnosEnRango = new List<GameObject>();

		// igualamos el momento del ultimo disparo al tiempo actual
		ultimoDisparo = Time.time;
		profesorDatos = gameObject.GetComponentInChildren<ProfesorDatos>();


	}
	

	void Update () {

		GameObject objetivo = null;

		// identificamos la distancia minima del enemigo con la distancia maxima
		// iteramos entre los enemigos en rango
		float distanciaMinimaEnemigo = float.MaxValue;
		foreach (GameObject enemigo in alumnosEnRango)
		{
			float distanciaAmeta = enemigo.GetComponent<MoverAlumno>().distanciaAmeta();

			// adquirimos un objetivo si su distancia a meta es menor que la minima actual
			if (distanciaAmeta < distanciaMinimaEnemigo)
			{
				objetivo = enemigo;
				distanciaMinimaEnemigo = distanciaAmeta;
			}
		}
		// si existe el objetivo
		if (objetivo != null)
		{
			// comprobamos si el proximo disparo llegara a tiempo
			if (Time.time - ultimoDisparo > profesorDatos.NivelActual.frecuenciaDisparo)
			{
				Disparo(objetivo.GetComponent<Collider2D>());
				ultimoDisparo = Time.time;
			}
			// utilizamos la posicion del objetivo para rotar al profesor en el eje Z
			Vector3 direccion = gameObject.transform.position - objetivo.transform.position;
			gameObject.transform.rotation = Quaternion.AngleAxis( Mathf.Atan2 (direccion.y, direccion.x) * 180 / Mathf.PI, new Vector3 (0, 0, 1));
		}

		
	}
}
