using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// definimos los 3 componentes que configuran una ola
public class Ola
{
	public GameObject alumnoPrefab;
	public float spawnIntervalo = 2;
	public int maxAlumnos = 20;
}


public class SpawnAlumnos : MonoBehaviour {

	public GameObject[] waypoints;



	public Ola[] olas;
	public int tiempoEntreOlas = 5;

	private GestorJuegoLogica gestorJuego;

	private float ultimoTiempoSpawn;
	private int alumnosSpawneados = 0;


	void Start () {

		// identificamos el ultimo tiempo de spawn con el tiempo actual, que será el que se cargue a la vez que la escena
		ultimoTiempoSpawn = Time.time;
		gestorJuego = GameObject.Find("GestorJuego").GetComponent<GestorJuegoLogica>();
				
	}
	


	void Update () {

		// Obtenemos el índice de la ola actual, y comprobamos si es el último.
		int olaActual = gestorJuego.Ola;

		// si es la penultima oleada, ocultamos el cartel de aviso para la siguiente
		if (olaActual + 1 >= olas.Length)
		{
			gestorJuego.ocultarSiguienteOla = true;

		}

		// si aun deben seguir los spawns  
		if (olaActual < olas.Length)
		{

			
			float tiempoIntervalo = Time.time - ultimoTiempoSpawn;
			float spawnIntervalo = olas[olaActual].spawnIntervalo;

			// comprobamos cuanto tiempo ha pasado desde el ultimo spawn para saber si lanzar otra ola
			// y comprobamos si hemos lanzado todos los enemigos de la ola actual
			if (((alumnosSpawneados == 0 && tiempoIntervalo > tiempoEntreOlas) || tiempoIntervalo > spawnIntervalo) && alumnosSpawneados < olas[olaActual].maxAlumnos)
			{
				// instanciamos el enemigo a partir del prefab y sumamos 1 al contador de enemigos lanzados
				ultimoTiempoSpawn = Time.time;
				GameObject nuevoAlumno = (GameObject)
					Instantiate(olas[olaActual].alumnoPrefab);
				nuevoAlumno.GetComponent<MoverAlumno>().waypoints = waypoints;
				alumnosSpawneados++;
			}
			// contamos el numero de objetos en pantalla mediante el tag "enemigo"
			if (alumnosSpawneados == olas[olaActual].maxAlumnos &&
				GameObject.FindGameObjectWithTag("Enemigo") == null)
			{
				// sumamos 1 a la ola y recalculamos el dinero, redondeando
				gestorJuego.Ola++;
				gestorJuego.Dinero = Mathf.RoundToInt(gestorJuego.Dinero * 1.1f);
				alumnosSpawneados = 0;
				ultimoTiempoSpawn = Time.time;
			}

		}
		// cuando se acaba con la ultima ola buscamos la etiqueta del juego ganado y cambiamos el booleano de gameOver
		else
		{
			//gestorJuego.ocultarSiguienteOla = true;
			gestorJuego.gameOver = true;
			GameObject gameOverText = GameObject.FindGameObjectWithTag ("Victoria");
			gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
		}

		
	}
}
