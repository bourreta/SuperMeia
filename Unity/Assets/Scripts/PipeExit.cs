using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeExit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){

		if (other.GetComponent<Player> () == null) {
			return;
		}
		TimeTracker.stopTime ();
		TimeTracker.saveFinalTime ();
		PlayerPrefs.SetInt ("score", ScoreTracker.getScore ());
		PlayerPrefs.SetInt("lives", LifeTracker.getLives());
		PlayerPrefs.SetInt ("Level2Unlocked", 1);
		PlayerPrefs.SetString ("NextLevel", "Level 2");
		SceneManager.LoadScene  ("LevelComplete");
	}
}
