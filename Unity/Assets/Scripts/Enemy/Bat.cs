using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Character
{
	public GameObject target; //the enemy's target
	public AudioClip damageSound;

	public override void Start()
	{
		base.Start();
		myAnimator = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player");
	}
	void Update()
	{

		if (seen && !GetIsDead ()) {
			Vector3 targetDir = target.transform.position - transform.position;
			float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 180);
			transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);
		} 

	}

	public virtual void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "SupergirlFoot")
		{
			// Destroy(gameObject);
			StartCoroutine(TakeDamage(10));
		}

		if (other.tag == "Laser")
		{
			//Destroy(gameObject);
			StartCoroutine(TakeDamage(10));
			//GetComponent<Collider2D>().enabled = false; //Turns off collisions so the player can't collide during the death animation.
			//StartCoroutine(Wait());
		}

		if (other.tag == "CameraEdge")
		{
			seen = true;
		}
	}


	//Destroys bad on first collision
	public virtual void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			StartCoroutine(TakeDamage(10));
			//moveSpeed = 0; //Stops the monster from moving when it dies
			//myAnimator.SetTrigger("die"); //Triggers the animation for death 
		}
	}

	public IEnumerator TakeDamage(int damage)
	{
		GetComponent<AudioSource>().clip = damageSound;
		GetComponent<AudioSource>().Play();
		health -= damage;
		if (!GetIsDead())
		{
			// myAnimator.SetTrigger("damage");
		}
		else
		{
			//Debug.Log (GetIsDead ());
			BeginDeath();
			yield return null;
		}
	}
}