using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Resource: https://www.youtube.com/watch?v=7ar4VRxlVbA, though many tutorials use this same script, so I'm not sure of exact origin

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing;
	private Player thePlayer;
	private bool resetting;
	private Vector3[] origBackgroundPositions;
	private Transform cam;
	//private Vector3 originalCam;
	private Vector3 previousCamPos;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<Player> ();
		cam = Camera.main.transform;
		//originalCam = cam.position;
		previousCamPos = cam.position;
		resetting = false;
		parallaxScales = new float[backgrounds.Length];
		origBackgroundPositions = new Vector3[backgrounds.Length];

		for(int i = 0; i < backgrounds.Length; i++){
			origBackgroundPositions [i] = backgrounds [i].position;
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}
	}

	void Update(){

	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!thePlayer.GetIsDead () && !resetting) {
			normalParallaxScroll ();
		} else if(thePlayer.GetIsDead() && !resetting) {
			resetting = true;
			Invoke ("resetParallaxScroll", 4.0625f);
		}

	}

	private void normalParallaxScroll(){
		for (int i = 0; i < backgrounds.Length; i++) {

			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			float backgroundTargetPosX = backgrounds [i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds [i].position.y, backgrounds [i].position.z);

			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime);

		}
		previousCamPos = cam.position;
	}

	private void resetParallaxScroll(){
		for (int i = 0; i < backgrounds.Length; i++) {
			backgrounds [i].position = origBackgroundPositions [i];
			previousCamPos = cam.position;
		}
		resetting = false;
	}
}
