using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthTracker : MonoBehaviour {
	
	//Text text;
	public Slider bar;
	private GameObject playerObj;
	private Player player;
	public GameObject healthCounter; 
	private Text text;


	void Start(){
		bar = GetComponent<Slider>();
		text = healthCounter.GetComponent<Text> ();
		if (SceneManager.GetActiveScene ().name != "LevelComplete") {
			playerObj = GameObject.Find ("Player");
			player = playerObj.GetComponent<Player>();
		}


	}

	void Update(){
		if (SceneManager.GetActiveScene ().name != "LevelComplete") {
			if (player.health < 0) {
				text.text = "" + 0;
			} else {
				text.text = "" + player.health;
			}

			if (player.health <= 50) {
				text.color = Color.black;
			} else {
				text.color = Color.white;
			}
			bar.value = player.health;
		}
	}



}
