using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour {
	public GameObject[] PickUpChoices;
	private bool pickUpSpawnInvoked = false;
	public float spawnDistanceX = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!pickUpSpawnInvoked) {
			pickUpSpawnInvoked = true;
			//spawn a random pick up on random interval
			Invoke("spawnPickup", Random.Range(15, 20));
		}
	}

	private void spawnPickup(){
		//select a random pick up from PickUpChoices and spawn it at a random location plus or minus spawnDistanceX from the gameObject
		Instantiate(PickUpChoices[Random.Range(0, PickUpChoices.Length)], new Vector3 (Random.Range(gameObject.transform.position.x - spawnDistanceX, gameObject.transform.position.x + spawnDistanceX), gameObject.transform.position.y, 0), Quaternion.identity);
		pickUpSpawnInvoked = false;
	}
}