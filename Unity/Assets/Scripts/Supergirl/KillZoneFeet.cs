using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneFeet : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    [SerializeField]
    private float bounceForce;
    public bool stomper = true;

	// Use this for initialization
	void Start ()
    {
        myRigidBody = transform.parent.GetComponent<Rigidbody2D>();
        myAnimator = transform.parent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        isStompingAllowed();
	}

    void OnTriggerEnter2D(Collider2D other)
	{//&& transform.parent.position.x - other.transform.position.x <= 0.1f && transform.parent.position.x - other.transform.position.x > -0.1f
		if (other.tag == "Enemy" && transform.parent.transform.position.y >= other.transform.position.y + 0.3f)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, bounceForce);
                myAnimator.SetTrigger("jump");
                myAnimator.SetBool("land", false);
            }
    }

    public void isStompingAllowed()
    {
        if (stomper == false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
