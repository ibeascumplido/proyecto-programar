using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cargamos el UI para acceder a clases como Text, que nos sirve para las etiquetas
using UnityEngine.UI;


public class GestorJuegoLogica : MonoBehaviour {

	// variables para los textos de información, dinero, olas, gameover, vidas
	public Text dineroTexto;

	public Text olaTexto;
	public GameObject[] siguienteOlaTextos;

	public bool gameOver = false;
	public bool ocultarSiguienteOla = false;


	public Text saludTexto;
	public GameObject[] saludIndicador;





	// variables private y public para almacenar el dinero total y para consultar y actualizar
	private int dinero;
	public int Dinero {
		get
		{ 
			return dinero;
		}
		set
		{
			dinero = value;

			// recuperamos el componente texto para la etiqueta, e imprimimos el texto y concatenamos con el valor
			dineroTexto.GetComponent<Text>().text = "Dinero: " + dinero;
		}


	}







	private int salud;
	public int Salud
	{
		get
		{
			return salud;
		}
		set
		{
			// utilizamos el script que sacude la cámara cada vez que se reduce la salud
			if (value < salud)
			{
				Camera.main.GetComponent<VibrarCamara>().Vibrar();
			}
			// actualizamos el texto del numero de edificios con el nuevo valor
			salud = value;
			saludTexto.text = "Salud: " + salud;

			// si la salud baja a cero y las oleadas aún no han terminado
			if (salud <= 0 && !gameOver)
			{
				// ponemos el booleano de GameOver a true e iniciamos animación
				gameOver = true;
				ocultarSiguienteOla = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			} 


			// eliminamos un edificio cuando la salud se reduzca
			for (int i = 0; i < saludIndicador.Length; i++)
			{
				if (i < Salud)
				{
					saludIndicador[i].SetActive(true);

				}
				else
				{
					
					saludIndicador[i].SetActive(false);

					// al eliminar el edificio, reproducimos el sonido
					AudioSource audioSource = gameObject.GetComponent<AudioSource>();
					audioSource.PlayOneShot(audioSource.clip);

				}
			}
		}
	}

	// mismo comportamiento que con el dinero

	private int ola;
	public int Ola
	{
		get
		{
			return ola;
		}
		set
		{
			ola = value;


			// comprobamos si se ha ocultado el cartel de siguiente ola, es decir, no hay mas spawn
			if (!ocultarSiguienteOla) {
				
				// iteramos sobre los textos y disparamos la animación
				for (int i = 0; i < siguienteOlaTextos.Length; i++) {
					siguienteOlaTextos [i].GetComponent<Animator> ().SetTrigger ("siguienteola");
				}
			} 

			// mostramos el texto y concatenamos, empezando en +1 para no mostrar un 0
			olaTexto.text = "Oleada: " + (ola + 1);
		}
	}


	// Valores con los que comienza el jugador
	void Start () {

		Ola = 0;

		Dinero = 1000;

		Salud = 5;

		// PRUEBAS PUNTUACION ***********************************************************************************************

		//public float puntuacionJuego = 1800.0f;

		//PlayerPrefs.SetInt("Score", Dinero);
		//int puntuacionJuego = PlayerPrefs.GetInt("Score");

	}
	

	void Update () {

		// PRUEBAS PUNTUACION ***********************************************************************************************
		int puntuacionmaxima = 0;
		if (Dinero > puntuacionmaxima) 
		{
			puntuacionmaxima = Dinero;
		}

		PlayerPrefs.SetInt("Score", puntuacionmaxima + ola*100 + Salud*100);
		int puntuacionJuego = PlayerPrefs.GetInt("Score");

				
	}
}
