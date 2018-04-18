using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndToMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("ChangeScene", 25);
	}

	private void ChangeScene(){
		SceneManager.LoadScene ("Main Menu");
	}
	

}
