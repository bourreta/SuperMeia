using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossGrowl : MonoBehaviour {

	private AudioSource bossClip;

	void Start(){
		bossClip = gameObject.GetComponent<AudioSource> ();
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> () == null) {
			return;
		}

		if (!bossClip.isPlaying) {
			bossClip.Play ();
		}

	}
}
