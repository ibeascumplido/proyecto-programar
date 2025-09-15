using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColocarProfe : MonoBehaviour {

	// Añadimos estas dos variables para crear una copia del objeto guardado en alumnoPrefab para crear un alumno y guardarlo en el GameObject alumno
	public GameObject profePrefab;
	private GameObject profesor;

	// asignamos variable para acceder al componente GestorLogicaJuego de la escena, cargado en Start()
	private GestorJuegoLogica gestorJuego;


	// metodo usado al empezar el juego, buscando y almacenando la logica del juego
	void Start () {

		gestorJuego = GameObject.Find("GestorJuego").GetComponent<GestorJuegoLogica>();
				
	}
	
	// metodo que se actualiza una vez por frame
	void Update () {
		
	}


	// averigua el precio segun el nivel que tenga el profesor, que no sea null y que tengamos más dinero que el precio que cuesta
	private bool comprobarProfe()
	{
		int cost = profePrefab.GetComponent<ProfesorDatos>().niveles[0].precio;
		return profesor == null && gestorJuego.Dinero >= cost;

	}



	// para saber si podemos mejorar un profesor
	private bool PosibleMejoraProfe()
	{
		// comprobamos si tenemos un profesor, si no devolvemos false
		if (profesor != null)
		{
			// obtenemos el nivel actual del profesor
			ProfesorDatos ProfesorDatos = profesor.GetComponent<ProfesorDatos>();
			ProfeNivel siguienteNivel = ProfesorDatos.GetSiguienteNivel();

			// si no retorna null es que tenemos disponible un nivel más, comprobamos que el dinero disponible es mayor ql el precio de mejora
			if (siguienteNivel != null)
			{
				return gestorJuego.Dinero >= siguienteNivel.precio;

			}
		}
		return false;
	}




	// coloca un profesor cuando el jugador clickea en el GameObject
	void OnMouseUp()
	{
		// primero comprobamos si existe ya un profesor colocado
		if (comprobarProfe())
		{
			// creamos el objeto profesor y lo instanciamos con el prefab, posición y rotación
			profesor = (GameObject) 
				Instantiate(profePrefab, transform.position, Quaternion.identity);
			
			// reproducimos el audio que teníamos vinculado al prefab
			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.PlayOneShot(audioSource.clip);

			// deducimos el precio del profesor de nuestro total
			gestorJuego.Dinero -= profesor.GetComponent<ProfesorDatos>().NivelActual.precio;

		}

		// además, si podemos mejorar al profesor, llamamos al método subirnivel() para subir 1
		else if (PosibleMejoraProfe())
		{
			profesor.GetComponent<ProfesorDatos>().subirNivel();

			// reproducimos el mismo audio que al colocarlo por primera vez
			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.PlayOneShot(audioSource.clip);

			// deducimos el precio del profesor de nuestro total
			gestorJuego.Dinero -= profesor.GetComponent<ProfesorDatos>().NivelActual.precio;

		}


	}







}
