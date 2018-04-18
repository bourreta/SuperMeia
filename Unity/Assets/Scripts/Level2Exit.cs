using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Exit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){

		if (other.GetComponent<Player> () == null) {
			return;
		}
		TimeTracker.stopTime ();
		TimeTracker.saveFinalTime ();
		PlayerPrefs.SetInt ("score", ScoreTracker.getScore ());
		PlayerPrefs.SetInt("lives", LifeTracker.getLives());
		PlayerPrefs.SetInt ("BossLevelUnlocked", 1); 
		PlayerPrefs.SetString ("NextLevel", "Boss Fight");
		SceneManager.LoadScene  ("LevelComplete");
	}
}
