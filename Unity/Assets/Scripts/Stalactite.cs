using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {
	private ParticleSystem partSystem;

	void Start(){
		partSystem = GetComponent<ParticleSystem> ();
	}

	private void OnCollisionEnter2D(Collision2D other){
		//shut off sprite renderer/collider, play particle effect and sound effect
		gameObject.GetComponent<AudioSource> ().Play ();
		gameObject.GetComponent<PolygonCollider2D>().enabled = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		partSystem.Play();
		//destroy the object on a 1 second delay to give above time to play
		Invoke("DestroyObject", 1);

	}

	private void DestroyObject(){
		Destroy (gameObject);
	}
}
