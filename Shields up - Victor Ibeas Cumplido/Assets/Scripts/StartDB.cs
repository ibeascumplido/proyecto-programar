using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDB : MonoBehaviour {

    public GameObject rankingManagerGO;

	// Use this for initialization
	void Start () {

        rankingManagerGO.GetComponent<RankingManager>().BorrarPuntosExtra();
        rankingManagerGO.GetComponent<RankingManager>().MostrarRanking();


    }
	
}
