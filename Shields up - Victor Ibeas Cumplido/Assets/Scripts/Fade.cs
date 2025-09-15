using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public Image fade;

	// llamamos al fundido al inicio de la scene
	void Start () {
        fade.CrossFadeAlpha(0, 1, true);
	}
	
}
