using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serializable provoca que las instancias de las clases sean editables desde el inspector de Unity, para ir modificando incluso con el juego en marcha.
[System.Serializable]



public class ProfeNivel
{
	public int precio;
	public GameObject vista;

	// variables relativas al proyectil lanzado
	public GameObject nota;
	public float frecuenciaDisparo;

}


public class ProfesorDatos : MonoBehaviour {

	// guardamos una lista que contiene objetos ProfeNivel
	public List<ProfeNivel> niveles;

	//variable para acceder desde la clase
	private ProfeNivel nivelActual;


	//variable para acceder desde fuera de la clase
	public ProfeNivel NivelActual
		{
		// devuelve el nivel actual de mejora del profesor
		get 
		{
			return nivelActual;
		}

		// establece el nivel de mejora del profesor
		set
		{
			// Primero cogemos el índice del nivel actual en el que se encuentra
			nivelActual = value;
			int indiceNivelActual = niveles.IndexOf(nivelActual);

			// Segundo, iteramos sobre todos los niveles y situamos la vista correspondiente en activa o inactiva según el indice del nivel actual
			GameObject vistaNivel = niveles[indiceNivelActual].vista;
			for (int i = 0; i < niveles.Count; i++)
			{
				if (vistaNivel != null) 
				{
					if (i == indiceNivelActual) 
					{
						niveles[i].vista.SetActive(true);
					}
					else
					{
						niveles[i].vista.SetActive(false);
					}
				}
			}
		}
	}


	// establecemos nivelActual tras la colocación de un profesor, asegurándose que se muestra el sprite correcto 
	void OnEnable()
	{
		NivelActual = niveles[0];
	}


	// comprobamos el nivel actual del profesor para saber si es el máximo y podemos mejorarlo o no.
	public ProfeNivel GetSiguienteNivel()
	{
		int indiceNivelActual = niveles.IndexOf (nivelActual);
		int indiceMaxNivel = niveles.Count - 1;
		if (indiceNivelActual < indiceMaxNivel)
		{
			return niveles[indiceNivelActual+1];
		} 
		else
		{
			return null;
		}
	}

	// cogemos el índice del nivel actual, comprobamos que es menor que el total de niveles -1, y mejoramos
	public void subirNivel()
	{
		int indiceNivelActual = niveles.IndexOf(nivelActual);
		if (indiceNivelActual < niveles.Count - 1)
		{
			NivelActual = niveles[indiceNivelActual + 1];
		}
	}


	void Start () {

	}


	void Update () {

	}






}
