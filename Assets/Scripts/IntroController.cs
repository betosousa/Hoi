using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			SceneManager.LoadScene("mapa");
		}
	
	}
}
