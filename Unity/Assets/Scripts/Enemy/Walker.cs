using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : Character
{
    // Use this for initialization
    public override void Start()
    {
        ChangeDirection(-1, 0.5f);
        facingRight = false;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Move();


		if (!GetIsDead() && facingRight && transform.localScale.x > 0) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		} else if (!GetIsDead() && !facingRight && transform.localScale.x < 0) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}

    }

    //Used to handle collisions
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SupergirlFoot")
        {
            StartCoroutine(TakeDamage(10));
        }
        if (other.tag == "Laser")
        {
            StartCoroutine(TakeDamage(10));
        }
        if (other.tag == "Edge")
        {
            ChangeDirection(-1, 1);
        }
    }

    public void Move()
    {
        transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        GetComponent<AudioSource>().Play();
        if (!GetIsDead())
        {
            // myAnimator.SetTrigger("damage");
        }
        else
        {
            StartDeath();
            yield return null;
        }
    }

    private void StartDeath()
    {
        moveSpeed = 0; //Stops the monster from moving when it dies
        myAnimator.SetTrigger("die"); //Triggers the animation for death 
        GetComponent<Collider2D>().enabled = false; //Turns off collisions so the player can't collide during the death animation.

		ScoreTracker.AddPoints (5);

        myRigidBody.gravityScale = 0; //Turns off gravity so that the body will not fall through the floor.
		if (transform.localScale.x < 0) {
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.9F);
        Death();
    }
    //Destroys the game object.
    private void Death()
    {
        Destroy(gameObject);
    }
}

