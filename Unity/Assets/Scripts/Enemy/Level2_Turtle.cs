using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Turtle : Character
{
    private float damage = 1;

    private float outOfShell = 3;
    [SerializeField]
    public Transform playerPrefab;
    public AudioClip damageSound;


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
        if (seen)
        {
            Move();
        }
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
            ChangeDirection(-1, 0.5F);
        }
        if (other.tag == "CameraEdge")
        {
            seen = true;
        }
    }

    public void Move()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        GetComponent<AudioSource>().clip = damageSound;
        GetComponent<AudioSource>().Play();
        if (!GetIsDead())
        {
            GoInShell();            
        }
        else
        {
            BeginDeath();
            yield return null;
        }
    }

    private void GoInShell()
    {
        moveSpeed = 0; //Stops the monster from moving when it dies
        myAnimator.SetTrigger("stomp"); //Triggers the animation for death 
        GetComponent<Collider2D>().enabled = false; //Turns off collisions so the player can't collide during the death animation.
        myRigidBody.gravityScale = 0; //Turns off gravity so that the body will not fall through the floor.
        StartCoroutine(Wait());
    }

    //Gives the monster time for the death animation to play before deleting itself.
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(damage);
        /*if (facingRight)
        {
            ChangeDirection(-1, 0.5f);
        }*/
        StartCoroutine(Wait2());
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(outOfShell);
        Death();
    }
    //Destroys the game object.
    private void Death()
    {
        moveSpeed = 1;
        GetComponent<Collider2D>().enabled = true;
        myRigidBody.gravityScale = 1;
    }
}