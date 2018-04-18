using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Flying : Character
{
    
    // Use this for initialization
    public override void Start()
    {
        facingRight = false;
        ChangeDirection(-1, 1);
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
    public void Move()
    {
        transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
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
            ChangeDirection(-1, 0.5F);
        }
        if (other.tag == "CameraEdge")
        {
            seen = true;
        }
    }

    public IEnumerator TakeDamage(int damage)
    {
        GetComponent<AudioSource>().Play();
        health -= damage;
        if(GetIsDead())
        {
            BeginDeath();
            yield return null;
        }
    }
}