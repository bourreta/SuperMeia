using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeTracker : MonoBehaviour {

	public static int currentLifeCount;
	private GameObject hud;
	private GameOver[] hudChildren;
	private GameOver gameOver;
	private Text lifeText;

	// Use this for initialization
	void Start () {

		lifeText = GetComponent<Text> ();
		//Scene scene = SceneManager.GetActiveScene ();

		hud = GameObject.Find("HUD");
		hudChildren = hud.GetComponentsInChildren<GameOver> (true);
		gameOver = hudChildren[0];
		currentLifeCount = PlayerPrefs.GetInt("lives");
	}
	
	// Update is called once per frame
	void Update () {
		lifeText.text = "" + currentLifeCount;

		if (currentLifeCount <= 0) {
			gameOver.setGameOver (true);
		}
	}
		
	public static int getLives(){
		return currentLifeCount;
	}

	public static void addLife(){
		currentLifeCount++;
	}

	public static void takeLife(){
		currentLifeCount--;
	}

	public static void setLives(int l){
		currentLifeCount = l;
		PlayerPrefs.SetInt ("lives", currentLifeCount);
	}
}
