using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	public string mainMenu;
	public static bool isGameOver;
	GameObject scoreCount;
	Text finalScoreText;
	public Text newHighScoreText;
	bool isHighScore;
	private int finalScore;
	bool updatingHighScore = false;

	// Use this for initialization
	void Start () {
		newHighScoreText.enabled = false;
		isHighScore = false;
		scoreCount = GameObject.Find ("GameOverScoreText");
		finalScoreText = scoreCount.GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		finalScoreText.text = "Score: " + ScoreTracker.getScore ().ToString();

		if (getIsGameOver () && !updatingHighScore) {
			updatingHighScore = true;
			Invoke ("updateHighScore", 0.5f);
		}
	}

	void FixedUpdate(){
		if (getIsGameOver () && isHighScore) {
			newHighScoreText.enabled = true;

			if (newHighScoreText.color == Color.white) {
				newHighScoreText.color = Color.red;
			} else {
				newHighScoreText.color = Color.white;
			}
		}
	}

	private void updateHighScore(){
		isHighScore = ScoreTracker.setHighScore ();

	}

	public void setGameOver(bool gO){
		isGameOver = gO;
		gameObject.SetActive (gO);
	}

	public static bool getIsGameOver(){
		return isGameOver;
	}

	public void RestartGame(){
		//restart from level 1
		LifeTracker.setLives (3);
		PlayerPrefs.SetInt("score", 0);
		ScoreTracker.setScore (0);
		SceneManager.LoadScene ("Level 1");
	}

	public void QuitToMainMenu(){
		//Quit to main
		SceneManager.LoadScene  (mainMenu);
	}
}
