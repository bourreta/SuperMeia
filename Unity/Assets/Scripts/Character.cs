using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {


    public Animator myAnimator;
    //public Animator MyAnimator { get; set; }
    protected Rigidbody2D myRigidBody;
    [SerializeField]
    public int health; //Health of the characters
    [SerializeField]
    public float moveSpeed; //Speed of characters running
    protected bool facingRight; //Direction of character
    protected bool seen = false;
    protected bool invincible = false;


    // Use this for initialization
    public virtual void Start () {
        facingRight = false;

        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

	}

    public void ChangeDirection(float x, float y)
    {
		/*
		if (gameObject.tag != "Player") {
			if (facingRight && transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			} else if (!facingRight && transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
		}*/
        facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, 1);
    }

    public bool GetIsDead()
    {
        return health <= 0;
    }

    protected void BeginDeath()
    {


		if (gameObject.tag != "Player" && transform.localScale.x < 0) {
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
        moveSpeed = 0; //Stops the monster from moving when it dies
        myAnimator.SetTrigger("die"); //Triggers the animation for death 
		if (gameObject.tag != "Player") {
			ScoreTracker.AddPoints (5);
		}
		transform.rotation = Quaternion.identity;
        GetComponent<BoxCollider2D>().enabled = false;
        myRigidBody.gravityScale = 0;
        StartCoroutine(onComplete());
    }

	IEnumerator onComplete(){
   

        while (myAnimator.GetBool("deathWord"))
        {
            //Debug.Log("Animation in progress!");
            yield return null;
        }
        Destroy(gameObject);
       //Debug.Log("Animation complete!");
    }
}
