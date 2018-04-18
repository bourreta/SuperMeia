using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelMenu : MonoBehaviour {

	public string level1;
	public string level2; 
	public string bossLevel;
	public string back;

	public Button Level2Button;
	public Button BossLevelButton;

	void Start(){
		if(PlayerPrefs.GetInt("BossLevelUnlocked") == 0){
			BossLevelButton.interactable = false;
		}

		if(PlayerPrefs.GetInt("Level2Unlocked") == 0){
			Level2Button.interactable = false;
		}
	}

	public void Level1()
	{
		LifeTracker.setLives (3);
		PlayerPrefs.SetInt("score", 0);
		SceneManager.LoadScene  (level1);

	}

	public void Level2(){
		LifeTracker.setLives (3);
		PlayerPrefs.SetInt("score", 0);
		SceneManager.LoadScene (level2);
	}

	public void FinalBoss(){
		LifeTracker.setLives (3);
		PlayerPrefs.SetInt("score", 0);
		SceneManager.LoadScene  (bossLevel);
	}

	public void Back(){
		//LifeTracker.setLives (4);
		SceneManager.LoadScene (back);
	}
}
