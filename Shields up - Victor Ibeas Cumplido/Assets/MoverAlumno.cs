using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAlumno : MonoBehaviour {

	// ocultamos en el Inspector las variables para proteger los valores, accesibles solo por codigo
	[HideInInspector]

	// almacenamos en un array los waypoints
	public GameObject[] waypoints;

	// trackea qué waypoints ha pasado ya el enemigo
	private int WaypointActual = 0;

	// guarda el tiempo en el que el enemigo ha pasado por el waypoint
	private float tiempoUltimoWaypoint;

	// velocidad del enemigo
	public float velocidad = 1.0f;



	public bool mirarderecha = true;

	private bool facingRight;

	// temporal del Vector para almacenar la dirección del eje X para voltear o no el sprite
	Vector3 temp;




	private void rotarAlumno()
	{
		// calculamos la direccion actual del enemigo restando la posicion actual del waypoint a la del siguiente
		Vector3 nuevaPosicionInicio = waypoints [WaypointActual].transform.position;
		Vector3 nuevaPosicionFin = waypoints [WaypointActual + 1].transform.position;
		Vector3 nuevaDireccion = (nuevaPosicionFin - nuevaPosicionInicio);

		// usamos Mathf.Atan2 para calcular el angulo de la nueva direccion en radianes, siendo cero la derecha, y convirtiendolo a grados
		float x = nuevaDireccion.x;
		float y = nuevaDireccion.y;
		float anguloRotacion = Mathf.Atan2 (y, x) * 180 / Mathf.PI;


		//según el ángulo Z (180,90 o resto) en el que se rota el sprite, lo calculamos para irlo ajustando al recorrido y cambiar la X a -1 para que mire en la otra dirección
		// solo cogemos el hijo del Sprite para que no vaya rotando las barras de vida
		if (anguloRotacion == 180) {

			// identificamos la variable sprite con el sprite del prefab, para rotar solamente eso y no todo el conjunto de sprite + barras de vida
			GameObject sprite = gameObject.transform.Find ("Sprite").gameObject;
			sprite.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);

			temp = transform.localScale;
			temp.x = -1;

			sprite.transform.localScale = temp;
		}

		else if (anguloRotacion == -90) {
			GameObject sprite = gameObject.transform.Find ("Sprite").gameObject;
			sprite.transform.rotation = Quaternion.AngleAxis (90, Vector3.forward);
			temp = transform.localScale;
			temp.x = -1;

			sprite.transform.localScale = temp;

		} else {
			
			GameObject sprite = gameObject.transform.Find ("Sprite").gameObject;
			sprite.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
			temp = transform.localScale;
			temp.x = 1;

			sprite.transform.localScale = temp;
		}

	}


	// clase encargada de calcular la distancia de los enemigos al edificio para priorizar objetivos de los profesores
	public float distanciaAmeta()
	{
		float distancia = 0;

		distancia += Vector2.Distance(gameObject.transform.position, waypoints [WaypointActual + 1].transform.position);

		for (int i = WaypointActual + 1; i < waypoints.Length - 1; i++)
		{
			// calculamos la diferencia entre dos Vector3 dados mediante Distance para saber la distancia restante
			Vector3 posicionInicio = waypoints [i].transform.position;
			Vector3 posicionFin = waypoints [i + 1].transform.position;
			distancia += Vector2.Distance(posicionInicio, posicionFin);
		}
		return distancia;
	}



	// inicializa el tiempo de último waypoint al tiempo actual, al empezar la escena
	void Start () {

		tiempoUltimoWaypoint = Time.time;
				
	}
	
	// usamos el update para el movimiento y rotacion
	void Update () {

		// recogemos las posiciones iniciales y finales del array de waypoints 
		Vector3 posicionInicio = waypoints [WaypointActual].transform.position;
		Vector3 posicionFin = waypoints [WaypointActual + 1].transform.position;

		// calculamos el tiempo necesario para el recorrido al siguiente waypoint mediante la distancia y velocidad
		// con Vector2.Lerp interpolamos la posición actual entre las posiciones inicio y fin de los segmentos
		float longitudCamino = Vector3.Distance (posicionInicio, posicionFin);
		float tiempoTotal = longitudCamino / velocidad;
		float tiempoActual = Time.time - tiempoUltimoWaypoint;
		gameObject.transform.position = Vector2.Lerp (posicionInicio, posicionFin, tiempoActual / tiempoTotal);


		// comprobamos si el enemigo ha alcanzado la posicion final
		if (gameObject.transform.position.Equals(posicionFin)) 
		{
			// si no la ha alcanzado 
			if (WaypointActual < waypoints.Length - 2)
			{
				// incrementamos el waypoint actual en uno y actualizamos el tiempo
				WaypointActual++;
				tiempoUltimoWaypoint = Time.time;

				// llamamos al metodo de rotacion
				rotarAlumno();

			}

			// si la ha alcanzado
			else
			{
				// destruimos uno de los edificios y reproducimos audio 
				Destroy(gameObject);

				AudioSource audioSource = gameObject.GetComponent<AudioSource>();
				AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

				// restamos salud
				GestorJuegoLogica gestorJuego =
					GameObject.Find("GestorJuego").GetComponent<GestorJuegoLogica>();
				gestorJuego.Salud -= 1;

			}
		}

		
	}




}
