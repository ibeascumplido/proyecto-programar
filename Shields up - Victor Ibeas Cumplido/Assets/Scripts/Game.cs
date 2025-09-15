using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Text puntosTXT;
    public Text nombreTXT;
    public GameObject panelGO;
    public GameObject rankingGO;
    int puntosDB;

	public void Start ()
	{
		// guardamos los puntos el las preferencias del jugador, que pueden ser llamadas
		// desde cualquier clase del programa
		// para poder enviarlo al ranking como string
		int puntos = PlayerPrefs.GetInt("Score");
		puntosDB = puntos;
		puntosTXT.text = puntos.ToString();

	}


    public void ActivarPanel()
    {
        panelGO.SetActive(true);
    }

    public void GuardarPuntosDB()
    {
        rankingGO.GetComponent<RankingManager>().InsertPuntos(nombreTXT.text, puntosDB);
        panelGO.SetActive(false);
    }
}
