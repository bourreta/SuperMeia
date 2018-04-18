using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour {
	string dateTimeStamp;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		dateTimeStamp = System.DateTime.Now.ToString("yy-MM-dd") + " " + System.DateTime.Now.ToString("hh_mm_ss");
		if(Input.GetKeyDown(KeyCode.O)){
			ScreenCapture.CaptureScreenshot("Screenshot-" + dateTimeStamp + ".png");
			Debug.Log ("SCREENSHOT TAKEN!");
			Debug.Log (dateTimeStamp);
		}
		//Debug.Log ("DATE AND TIME");
		//Debug.Log (dateTimeStamp);
		//Debug.Log(Application.persistentDataPath);


	}
}
