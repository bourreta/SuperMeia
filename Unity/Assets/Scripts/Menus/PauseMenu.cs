using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//resource: https://www.youtube.com/watch?v=Wrelb5WBnoQ

public class PauseMenu : MonoBehaviour {
	public string mainMenu;

	public bool isPaused;
	private bool soundPlayed;
	public GameObject pauseMenuCanvas;


	void Update(){
		if (isPaused) {
			if (!soundPlayed) {
				GetComponent<AudioSource>().Play ();
				soundPlayed = true;
			}
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} else {
			soundPlayed = false;
			pauseMenuCanvas.SetActive (false);
			Time.timeScale = 1f;
		}

		if (Input.GetKeyDown(KeyCode.Escape)){
			isPaused = !isPaused;
		}
	}

	public void Resume(){
		isPaused = false;
	}

	public void QuitToMainMenu(){
		SceneManager.LoadScene  (mainMenu);
	}
}
