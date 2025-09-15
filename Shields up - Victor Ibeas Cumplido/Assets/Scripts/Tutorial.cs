
using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{

	// disparamos el trigger que inicia la animacion de fundido para mostrar los mensajes de ayuda
    public void Ayuda()
    {
		gameObject.GetComponent<Animator>().SetTrigger("ayudar");
    }

}
