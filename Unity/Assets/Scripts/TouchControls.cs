using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {
	private Player player;
	public bool jumpButtonDown;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
	}
	
	public void leftArrow(){
		player.Move (-1);
	}

	public void rightArrow(){
		player.Move (1);
	}

	public void jump(){
		jumpButtonDown = true;
		player.Jump ();
	}

	public void shoot(){
		if (player.isPoweredUp) {
			player.Shoot ();
		}
	}

	public void unpressedJump(){
		jumpButtonDown = false;
	}
		
	public void unpressedArrow(){
		if (player.airControl) {
			player.Move (0);
		} 
	}

	public void pause(){
		FindObjectOfType<PauseMenu> ().isPaused = !FindObjectOfType<PauseMenu>().isPaused;
	}
}
