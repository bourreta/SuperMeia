using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){

		if (other.GetComponent<Player> () == null) {
			return;
		}
		gameObject.GetComponent<AudioSource> ().Play ();
		gameObject.GetComponent<ParticleSystem> ().Pause ();
		gameObject.GetComponent<ParticleSystem> ().Clear ();
		gameObject.GetComponent<PolygonCollider2D>().enabled = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;

		Destroy (gameObject, 5);
	}


}