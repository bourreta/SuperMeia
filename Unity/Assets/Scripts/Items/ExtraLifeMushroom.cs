using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeMushroom : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player") {
			LifeTracker.addLife ();
			PlayerPrefs.SetInt("lives", LifeTracker.getLives());

			gameObject.GetComponent<AudioSource> ().Play ();
			gameObject.GetComponent<ParticleSystem> ().Pause ();
			gameObject.GetComponent<ParticleSystem> ().Clear ();
			gameObject.GetComponent<PolygonCollider2D>().enabled = false;
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;

			Destroy (gameObject, 5);
		}
	}
}
