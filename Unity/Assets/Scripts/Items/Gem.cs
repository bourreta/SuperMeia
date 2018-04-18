using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public int pointsToAdd = 10;

	void OnTriggerEnter2D(Collider2D other){
		
		if (other.GetComponent<Player> () == null) {
			return;
		}
		ScoreTracker.AddPoints (pointsToAdd);

		gameObject.GetComponent<AudioSource> ().Play ();
		gameObject.GetComponent<PolygonCollider2D>().enabled = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;

		Destroy (gameObject, 1f);
	}



}
