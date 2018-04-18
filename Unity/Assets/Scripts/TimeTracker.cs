using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour {
	public Text timeText;
	private static float startTime;
	public static float time;
	public static float totalTime = 300;
	private static float elapsedTime;
	private static bool stoppedTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		stoppedTime = false;
	}

	// Update is called once per frame
	void Update () {
		if (!stoppedTime) {
			elapsedTime = Time.time - startTime;
			time = totalTime - elapsedTime;

			if (time <= 0) {
				timeText.text = "" + 0; 
			} else {
				timeText.text = "" + (int)time;
			}

			if (time <= 30) {
				timeText.color = Color.red;
			} else {
				timeText.color = Color.white;
			}

		}
	}

	public static void resetTimeAtLastCheckpoint(){
		startTime = Time.time - PlayerPrefs.GetFloat("timeAtLastCheckpoint") + startTime;
	}

	public static void saveTime(){
		PlayerPrefs.SetFloat ("timeAtLastCheckpoint", Time.time);
	}

	public static int getTime(){
		return (int)time;
	}

	public static float getTimeAsFloat(){
		return time;
	}

	public static void stopTime(){
		stoppedTime = true;
	}

	public static void saveFinalTime(){
		PlayerPrefs.SetFloat ("finalTimeLastLevel", time);
	}

	public static int getFinalTime(){
		return (int)PlayerPrefs.GetFloat ("finalTimeLastLevel");
	}
}
