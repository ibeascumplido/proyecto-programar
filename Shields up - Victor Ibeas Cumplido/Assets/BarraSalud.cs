using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraSalud : MonoBehaviour {

	// variables para la salud máxima, actual y la escala original
	public float saludMaxima = 100;
	public float saludActual = 100;
	private float escalaOriginal;


	// nada mas empezar el juego, con la salud maxima, guardamos ese valor del localscale
	void Start () {

		escalaOriginal = gameObject.transform.localScale.x;

		
	}
	
	// iniciamos una variable temporal para las nuevas escalas, que averiguamos operando con la salud
	void Update () {

		Vector3 escalaTemporal = gameObject.transform.localScale;
		escalaTemporal.x = saludActual / saludMaxima * escalaOriginal;
		gameObject.transform.localScale = escalaTemporal;
				
	}
}
