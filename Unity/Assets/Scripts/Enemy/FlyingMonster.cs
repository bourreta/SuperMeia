using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : Character {

    [SerializeField]
    private float death;
    private Vector2 pos1;
    [SerializeField]
    private float vertical;
    [SerializeField]
    private float horizontal;
    private Vector2 pos2;
    public Transform playerPrefab;

    // Use this for initialization
    public override void Start()
    {
        pos1 = transform.position;
        pos2 = new Vector2(transform.position.x + horizontal, transform.position.y + vertical);
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
       transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
    }

    //Gives the monster time for the death animation to play before deleting itself.
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(death);
        Death();
    }

    //Destroys the game object.
    private void Death()
    {
        Destroy(gameObject);
    }

    //Used to handle when supergirl jumps on the monsters head.
    public virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "SupergirlFoot")
        {
            GetComponent<AudioSource>().Play();
            //Keeps the monster from moving on death. 
            pos1 = transform.position;
            transform.position = pos1;
            moveSpeed = 0; //Stops the monster from moving when it dies
            myAnimator.SetTrigger("die"); //Triggers the animation for death 
			ScoreTracker.AddPoints (5);
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
            GetComponent<Collider2D>().enabled = false; //Turns off collisions so the player can't collide during the death animation.
            StartCoroutine(Wait());

        }
        if (other.tag == "Laser")
        {
            GetComponent<AudioSource>().Play();
            //Keeps the monster from moving on death. 
            pos1 = transform.position;
            transform.position = pos1;
            moveSpeed = 0; //Stops the monster from moving when it dies
            myAnimator.SetTrigger("die"); //Triggers the animation for death 
			ScoreTracker.AddPoints (5);
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
            GetComponent<Collider2D>().enabled = false; //Turns off collisions so the player can't collide during the death animation.
            StartCoroutine(Wait());

        }
    }
}
