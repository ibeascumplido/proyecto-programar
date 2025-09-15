

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public string escena = "Jugar";

	void RestartLevel()
    {
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		SceneManager.LoadScene (escena);
    }



}
