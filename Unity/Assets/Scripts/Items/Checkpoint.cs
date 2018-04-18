using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public LevelManager levelManager;
	protected Animator myAnimator;
	private AudioSource clip;
	bool checkpointReached;

	void Start(){
		if (gameObject.name != "InvisibleCheckpoint") {
			myAnimator = GetComponent<Animator>();
			myAnimator.SetBool ("SetFlagUp", false);
			clip = GetComponent<AudioSource> ();
		}
		checkpointReached = false;

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player") {

			if (gameObject.name != "InvisibleCheckpoint" && !checkpointReached) {
				myAnimator.SetBool ("SetFlagUp", true);
				clip.Play ();
			}
			if (!checkpointReached) {
				levelManager.currentCheckpoint = gameObject;
				PlayerPrefs.SetInt ("lastCheckpointScore", ScoreTracker.getScore ());
				TimeTracker.saveTime ();
				checkpointReached = true;
			}

		}
	}
}
