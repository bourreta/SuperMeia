using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Character
{
    public AudioClip damageSound;

    // Use this for initialization
    public override void Start()
    {
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
    }

    //Used to handle collisions
    public virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "SupergirlFoot")
        {
            //Debug.Log("Trigger");
            StartCoroutine(TakeDamage(10));
        }
        if (other.tag == "Laser")
        {
            StartCoroutine(TakeDamage(10));
        }
        if (other.tag == "Edge")
        {
			//Debug.Log ("HITTING AN EDGE");
            ChangeDirection(-1, 0.5F);
        }
        if (other.tag == "CameraEdge")
        {
            seen = true;
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
        GetComponent<AudioSource>().clip = damageSound;
        GetComponent<AudioSource>().Play();
        health -= damage;
        if (!GetIsDead())
        {
            // myAnimator.SetTrigger("damage");
        }
        else
        {
            BeginDeath();
            yield return null;
        }
    }
}

