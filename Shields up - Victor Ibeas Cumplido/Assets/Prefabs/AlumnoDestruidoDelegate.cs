using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlumnoDestruidoDelegate : MonoBehaviour {


	public delegate void AlumnoDelegate (GameObject enemigo);
	public AlumnoDelegate alumnoDelegate;


	void onDestroy()
	{
		if (alumnoDelegate != null)
		{
			alumnoDelegate(gameObject);
		}
	}



	void Start () {
		
	}
	

	void Update () {
		
	}
}
