using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Resource: https://www.youtube.com/watch?v=JKoBWBXVvKY
public class BackgroundMusic : MonoBehaviour {
	Scene scene;
	// Use this for initialization
	void Start () {
		GameObject[] musicObjs = GameObject.FindGameObjectsWithTag ("MenuMusic");
		if (musicObjs.Length > 1) {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
	}

	void Update(){
		scene = SceneManager.GetActiveScene ();
		if (scene.name == "Level 1" || scene.name == "Level 2" || scene.name == "Boss Fight") {
			Destroy (gameObject);
		}
	}
	

}
