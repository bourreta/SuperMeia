using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string startLevel;
	public string highScore;
	public string loadLevel;
	public string howToPlay;

	void Start(){
		//make high score prefs
		for (int i = 1; i <= 5; i++) {
			if (!PlayerPrefs.HasKey ("HighScore" + i.ToString())) {
				PlayerPrefs.SetInt ("HighScore" + i.ToString(), 0);
			}
		}


		if (!PlayerPrefs.HasKey ("Level2Unlocked")) {
			PlayerPrefs.SetInt ("Level2Unlocked", 0);
		}
			
		if (!PlayerPrefs.HasKey ("BossLevelUnlocked")) {
			PlayerPrefs.SetInt ("BossLevelUnlocked", 0);
		}

	}

	public void NewGame()
	{
		LifeTracker.setLives (3);
		PlayerPrefs.SetInt("score", 0);
		SceneManager.LoadScene (startLevel);
	}

	public void HowToPlay(){
		SceneManager.LoadScene (howToPlay);
	}

	public void LoadLevel(){
		SceneManager.LoadScene (loadLevel);
	}

	public void HighScore(){
		SceneManager.LoadScene (highScore);
	}
		
	public void QuitGame (){
		Application.Quit ();
	}
}
