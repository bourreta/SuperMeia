using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeManager : MonoBehaviour {
	public GameObject[] spikes;
	protected GameObject theSpike;
	private Transform playerPos; 
	float randX, randY;
	int randSpike;
	int randSeconds;
	bool spikeExists = false; 
	bool spikeIsFalling = false;
	// Use this for initialization
	float shakeOffset = 0.15f;
	bool shakeSlow = true;
	private Transform shakeStart;
	private Transform shakeEnd;
	public int spawnSecondsLowRange = 5;
	public int spawnSecondsHighRange = 10;
	public float spawnDistanceX = 8f;
	public float fallDelay = 3f;
	public float gravityFactor = 0.5f;
	void Start () {
		playerPos = FindObjectOfType<Player> ().GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {
		
		//make a new spike on a random delay between specified range of seconds when within range of the player
		if (!spikeExists && Mathf.Abs(playerPos.position.x - gameObject.transform.position.x) < 13 && Mathf.Abs(playerPos.position.y - gameObject.transform.position.y) < 6){
			randSpike = Random.Range (0, 3);
			randSeconds = Random.Range (spawnSecondsLowRange, spawnSecondsHighRange);
			spikeExists = true;
			Invoke ("MakeSpike", randSeconds);
		}


	}

	void FixedUpdate(){
		//Make the spike shake back and forth if it is hanging
		if (theSpike != null && !spikeIsFalling && shakeSlow) {
			theSpike.transform.position = Vector3.Lerp (theSpike.transform.position, new Vector3 (theSpike.transform.position.x + shakeOffset, theSpike.transform.position.y), 50.0f);
			shakeOffset *= -1;
			shakeSlow = false;
		}
		shakeSlow = true;
	}

	private void MakeSpike(){
		//create the spike relative to the FallingSpikeManager at a random x position spawnDistanceX away.
		theSpike = Instantiate (spikes[randSpike], new Vector3 (Random.Range(gameObject.transform.position.x - spawnDistanceX, gameObject.transform.position.x + spawnDistanceX), gameObject.transform.position.y, 0), Quaternion.identity);
		theSpike.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
		theSpike.GetComponent<PolygonCollider2D>().enabled = false;
		spikeIsFalling = false;

		//let spike hang for fallDelay seconds, then drop it
		Invoke ("DropSpike", fallDelay);

	}

	private void DropSpike(){

		//drop the spike by turning its gravity on
		spikeIsFalling = true;
		theSpike.GetComponent<Rigidbody2D> ().gravityScale = gravityFactor;
		theSpike.GetComponent<PolygonCollider2D>().enabled = true;

		//give spike X seconds to fall
		Invoke ("SpikeFallTime", 3);
	}

	private void SpikeFallTime(){

		//allow another spike to fall by reseting bool
		spikeExists = false;
	}
}
