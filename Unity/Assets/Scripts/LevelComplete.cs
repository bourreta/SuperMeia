using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {
	public Text InitialScore;
	public Text InitialScore2;
	public Text TimeRemaining;
	public Text BonusTime;
	public Text BonusPercent;
	public Text Multiplier;
	public Text FinalScore;
	float timeRemaining;
	float bonusPercent;
	float multiplier;
	float score; 
	int finalScore;
	public AudioClip music;
	public AudioClip calculations;
	private bool isHighScore;
	public Text newHighScore;
	// Use this for initialization
	void Start () {
		isHighScore = false;
		newHighScore.enabled = false;
		score = PlayerPrefs.GetInt ("score");
		timeRemaining = TimeTracker.getFinalTime ();

		bonusPercent = timeRemaining / 300;
		multiplier = 1 + bonusPercent;
		finalScore = (int)(multiplier * score);
		PlayerPrefs.SetInt ("score", finalScore);
		ScoreTracker.setScore (finalScore);

		GetComponent<AudioSource> ().clip = music;
		GetComponent<AudioSource> ().Play ();

		Invoke ("CalculateScore", 3);
	}

	void FixedUpdate(){
		if(PlayerPrefs.GetString("NextLevel") == "End" && isHighScore){
			newHighScore.enabled = true;
			Debug.Log ("NEW HIGH SCORE");
			if (newHighScore.color == Color.white) {
				newHighScore.color = Color.red;
			} else {
				newHighScore.color = Color.white;
			}
		}
	}

	private void CalculateScore(){
		GetComponent<AudioSource> ().clip = calculations;
		GetComponent<AudioSource> ().Play ();
		InitialScore.text = "" + score;
		Invoke ("TimeRemain", 1);
	}

	private void TimeRemain(){
		GetComponent<AudioSource> ().Play ();
		TimeRemaining.text = "" + timeRemaining;
		Invoke ("Bonus", 1);
	}

	private void Bonus(){
		GetComponent<AudioSource> ().Play ();
		BonusTime.text = "" + timeRemaining;
		BonusPercent.text = "" + bonusPercent.ToString("F2");
		Invoke ("Final", 1);
	}

	private void Final(){
		GetComponent<AudioSource> ().Play ();
		InitialScore2.text = "" + score;
		Multiplier.text = "" + multiplier.ToString("F2");
		FinalScore.text = "" + finalScore;

		if (PlayerPrefs.GetString ("NextLevel") == "End") {
			isHighScore = ScoreTracker.setHighScore ();
		}

		Invoke("NextScene", 5);
	}

	private void NextScene(){
		SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel"));
	}
}
