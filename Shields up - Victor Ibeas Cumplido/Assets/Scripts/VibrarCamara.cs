using UnityEngine;
using System.Collections;

public class VibrarCamara : MonoBehaviour
{
	// transformacion del objeto
	private Transform transformar;

	// duracion del movimiento
	private float vibrarDuracion = 0f;

	// fuerza del movimiento
	private float vibrarFuerza = 0.1f;

	// velocidad de la vibracion
	private float vibrarVelocidad = 0.2f;

	// posicion inicial del objeto
	Vector3 posicionInicio;


	// iniciamos el objeto sin transformar
	void Awake()
	{
		if (transformar == null)
		{
			transformar = GetComponent(typeof(Transform)) as Transform;
		}
	}


	void OnEnable()
	{
		posicionInicio = transformar.localPosition;
	}

	// modificamos la posicion sumandole el random con la fuerza que hayamos especificado
	void Update()
	{
		if (vibrarDuracion > 0)
		{
			transformar.localPosition = posicionInicio + Random.insideUnitSphere * vibrarFuerza;

			vibrarDuracion -= Time.deltaTime * vibrarVelocidad;
		}
		else
		{
			vibrarDuracion = 0f;
			transformar.localPosition = posicionInicio;
		}
	}


	// metodo al que llamamos, estableciendo la duracion
	public void Vibrar() {
		vibrarDuracion = 0.2f;
	}

}