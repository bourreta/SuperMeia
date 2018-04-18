using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

	public static int score;

	Text text;
	Scene scene;

	void Start(){
		text = GetComponent<Text> ();
		scene = SceneManager.GetActiveScene ();
		score = PlayerPrefs.GetInt ("score");

	}

	void Update(){
		if (score < 0) {
			score = 0;
		}
		if (scene.name != "LevelComplete") {
			text.text = "" + score;
			PlayerPrefs.SetInt ("score", score);
		} else {
			text.text = "";
		}
			
	}



	public static void AddPoints (int pointsToAdd){
		score += pointsToAdd;
	}

	public static int getScore(){
		return score;
	}

	public static void setScore(int s){
		score = s;
	}

	public static bool setHighScore(){
		bool isHighScore = false;
		int i = 1;
		int position = -1;
		while (i <= 5) {
			if (PlayerPrefs.GetInt ("score") > PlayerPrefs.GetInt ("HighScore" + i.ToString ()) && position < 0) {
				isHighScore = true;
				position = i;
			}
			i++;
		}

		if (isHighScore) {
			i = 5;
			while (i > position) {
				PlayerPrefs.SetInt("HighScore" + i.ToString(), PlayerPrefs.GetInt("HighScore" + (i - 1)));
				i--;
			}
			PlayerPrefs.SetInt("HighScore" + position.ToString(), ScoreTracker.getScore());
		}

		return isHighScore;
	}
}

