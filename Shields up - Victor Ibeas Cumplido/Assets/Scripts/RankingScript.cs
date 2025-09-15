using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScript : MonoBehaviour {


    public GameObject Posicion;
    public GameObject Nombre;
    public GameObject Puntos;

	public void PonerPuntos(string pos, string nombre, string puntos){


        this.Posicion.GetComponent<Text>().text = pos;
        this.Nombre.GetComponent<Text>().text = nombre;
        this.Puntos.GetComponent<Text>().text = puntos;
    }
}
