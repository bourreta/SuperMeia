using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject ExpendableObjects_StartGameState;
	public GameObject LevelObjects;
	private GameObject ExpendableObjects_RunningGameState;
	public GameObject currentCheckpoint;
	private LifeTracker lifeTracker;
	bool endingBossLevel = false;
	private Player player;
	public GameObject boss;
	Scene scene;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
		scene = SceneManager.GetActiveScene();

		//Set up expendable objects
		ExpendableObjects_StartGameState.SetActive(false);
		ExpendableObjects_RunningGameState = Instantiate (ExpendableObjects_StartGameState);
		ExpendableObjects_RunningGameState.transform.parent = LevelObjects.transform;
		ExpendableObjects_RunningGameState.SetActive (true);
		if (scene.name == "Boss Fight") {
			boss = GameObject.Find ("Boss");
		}
	}

	void LateUpdate(){
		//Change scene to "end" if final boss is killed
		if (scene.name == "Boss Fight" && !endingBossLevel) {
			if (boss.GetComponent<Boss> ().GetIsDead ()) {
				endingBossLevel = true;
				Invoke ("endBossLevel", 3);
			}
		}
		//stop time if game is over
		if (player.GetIsDead () && LifeTracker.getLives () - 1 <= 0) {
			TimeTracker.stopTime ();
		}

	}

	public void RespawnPlayer()
	{

		//reset time
		TimeTracker.resetTimeAtLastCheckpoint();
		TimeTracker.saveTime ();
		//respawn all item pickups and enemies
		Destroy (ExpendableObjects_RunningGameState);
		ExpendableObjects_RunningGameState = Instantiate (ExpendableObjects_StartGameState);
		ExpendableObjects_RunningGameState.transform.parent = LevelObjects.transform;
		ExpendableObjects_RunningGameState.SetActive (true);

		//reset new boss, if boss level
		if (scene.name == "Boss Fight") {
			boss = GameObject.FindObjectOfType<Boss>().gameObject;
		}


		//reset player score to last checkpoint
		ScoreTracker.setScore(PlayerPrefs.GetInt("lastCheckpointScore"));


		//respawn player at last checkpoint location, update life count
		player.transform.position = currentCheckpoint.transform.position;
		PlayerPrefs.SetInt ("lives", LifeTracker.getLives ());

	}

	private void endBossLevel(){
		TimeTracker.stopTime ();
		TimeTracker.saveFinalTime ();
		PlayerPrefs.SetInt ("score", ScoreTracker.getScore ());
		PlayerPrefs.SetInt("lives", LifeTracker.getLives());
		PlayerPrefs.SetString ("NextLevel", "End");
		SceneManager.LoadScene  ("LevelComplete");
	}

}
