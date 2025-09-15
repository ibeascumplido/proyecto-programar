using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasManejo : MonoBehaviour {

	// variables para controlar el comportamiento de los proyectiles
	public float velocidad = 10;
	public int daño;
	public GameObject objetivo;
	public Vector3 posicionInicio;
	public Vector3 posicionObjetivo;

	private float distancia;
	private float tiempoInicio;

	// el gestor se encargará de sumar dinero al acabar con un enemigo
	private GestorJuegoLogica gestorJuego;



	void Start () {

		tiempoInicio = Time.time;
		distancia = Vector2.Distance (posicionInicio, posicionObjetivo);
		GameObject gm = GameObject.Find("GestorJuego");
		gestorJuego = gm.GetComponent<GestorJuegoLogica>();

		
	}
	

	void Update () {

		// calculamos la nueva posicion del proyectil con Lerp interpolando entre posicion inicial y final
		float timeInterval = Time.time - tiempoInicio;
		gameObject.transform.position = Vector3.Lerp(posicionInicio, posicionObjetivo, timeInterval * velocidad / distancia);


		// rotacion de proyectiles
		Vector3 dir = objetivo.transform.position - gameObject.transform.position;

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



		// si el proyectil iguala su posicion a la del objetivo
		if (gameObject.transform.position.Equals(posicionObjetivo))
		{
			// debemos comprobar que el objetivo aun existe
			if (objetivo != null)
			{
				// recuperamos la barra de salud del objetivo y le restamos la cantidad de daño del proyectil
				Transform barraSaludTransformar = objetivo.transform.Find("BarraSalud");
				BarraSalud barraSalud = barraSaludTransformar.gameObject.GetComponent<BarraSalud>();
				barraSalud.saludActual -= Mathf.Max(daño, 0);

				// si la salud del objetivo llega a cero, lo destruimos, reproducimos audio y sumamos dinero
				if (barraSalud.saludActual <= 0)
				{
					Destroy(objetivo);
					// seguimos reproduciendo el sonido de muerte de los enemigos
					AudioSource audioSource = objetivo.GetComponent<AudioSource>();
					AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

					gestorJuego.Dinero += 50;
				}
			}

			// destruimos el objeto para que el proyectil no siga atravesando la pantalla de juego
			Destroy(gameObject);
		}

		
	}
}
