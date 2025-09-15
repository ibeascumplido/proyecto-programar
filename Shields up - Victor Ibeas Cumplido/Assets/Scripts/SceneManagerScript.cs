using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

	// cargamos el fundido para el cambio de escena
    public Image fade;
    public void CambioEscena(string es)
    {
        fade.CrossFadeAlpha(1, 1, true);
        StartCoroutine(ActivoFade(es));
    }

	// temporizamos el cambio de escena para que de tiempo al fundido
    IEnumerator ActivoFade(string e)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(e);
    }

	// cerramos el juego
	public void Salir()
	{
		Application.Quit();

	}
}
