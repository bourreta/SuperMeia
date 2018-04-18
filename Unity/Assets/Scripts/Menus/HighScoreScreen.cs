using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreScreen : MonoBehaviour {

	public string mainMenu;
	public GameObject scoreParent;
	public Text[] scorePlaceHolders;

	void Start(){

		for (int i = 0; i < 5; i++) {
			if(PlayerPrefs.HasKey("HighScore" + (i + 1))){
				scorePlaceHolders[i].text += "" + PlayerPrefs.GetInt("HighScore" + (i + 1));
			}
			else{
				scorePlaceHolders [i].text += "0";
			}

		}
	}

	public void QuitToMainMenu(){
		SceneManager.LoadScene  (mainMenu);
	}
}
