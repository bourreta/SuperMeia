using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character {

    private IEnemyState currentState;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    [SerializeField]
    private LayerMask whatIsGround; //What the groundPoints consider ground to be able to jump off of
    public bool isGrounded;
    private int touchingGround;
    [SerializeField]
    private Transform[] groundPoints; //Used to determine if the player character is grounded for the use of jumping
    [SerializeField]
    private float groundRadius; //Size of the groundpoints above

    [SerializeField]
    private GameObject tailShotPrefab;
    [SerializeField]
    private GameObject doubletailShotPrefab;
    [SerializeField]
    public Transform tail;
    [SerializeField]
    public Transform tail2;

    public bool enraged = false;

    [SerializeField]
    private float invincible_time;

    public float flashSpeed;
    SpriteRenderer spRndrer;

    public AudioClip shootSound;
    public AudioClip damageSound;
	public AudioClip hitSound;
    public AudioClip jumpSound;


    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        spRndrer = GetComponent<SpriteRenderer>();
        ChangeState(new IdleState());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health > 0)
        {
            currentState.Execute();
            //Improves the feel of the jump
            if (myRigidBody.velocity.y < 0)
            { myRigidBody.gravityScale = fallMultiplier; }
            else if (myRigidBody.velocity.y > 0)
            { myRigidBody.gravityScale = lowJumpMultiplier; }
            else { myRigidBody.gravityScale = 1; }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    //Moves the boss from left to right
    public void Move()
    {
        myAnimator.SetFloat("Speed", 1);
        transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));

    }

    //Cause the boss to jump
    public void Jump()
    {
        //Debug.Log("JUMP");
        GetComponent<AudioSource>().clip = jumpSound;
        GetComponent<AudioSource>().Play(); 
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000), ForceMode2D.Impulse);
        myAnimator.SetTrigger("Jump");
    }


    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public bool IsGrounded()
    {
        //Debug.Log("I am grounded");
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        touchingGround++;
                    }
                    if (touchingGround != 0)
                    {
                        myAnimator.ResetTrigger("Jump");
                        //Debug.Log("I am grounded: TRUE");
                        return true;
                    }
                }
        }
        //Debug.Log("I am grounded: FALSE");
        return false;
    }


    public void Shoot()
    {
        GameObject tmp;

        if (facingRight)
        {
            //Debug.Log("Right");
            GetComponent<AudioSource>().clip = shootSound;
            GetComponent<AudioSource>().Play();
            myAnimator.SetTrigger("Tail");
            tmp = (GameObject)Instantiate(tailShotPrefab, tail.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Boss_TailShot>().Initialize(Vector2.right);

        }
        else
        {
            GetComponent<AudioSource>().clip = shootSound;
            GetComponent<AudioSource>().Play();
            myAnimator.SetTrigger("Tail");
            tmp = (GameObject)Instantiate(tailShotPrefab, tail.position, Quaternion.Euler(new Vector3(0, 0, 360)));
            tmp.GetComponent<Boss_TailShot>().Initialize(Vector2.left);
        }
    }

    public void DoubleShoot()
    {
        GameObject tmp1;
        GameObject tmp2;
        GetComponent<AudioSource>().clip = shootSound;
        GetComponent<AudioSource>().Play();
        if (facingRight)
        {
            //Debug.Log("Right");
            myAnimator.SetTrigger("Tail");
            tmp1 = (GameObject)Instantiate(doubletailShotPrefab, tail.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp1.GetComponent<Boss_TailShot>().Initialize(Vector2.right);
            myAnimator.SetTrigger("Tail");
            tmp2 = (GameObject)Instantiate(doubletailShotPrefab, tail2.position, Quaternion.Euler(new Vector3(0, -180, 180)));
            tmp2.GetComponent<Boss_TailShot>().Initialize(Vector2.left);
        }
        else
        {
            myAnimator.SetTrigger("Tail");
            tmp1 = (GameObject)Instantiate(doubletailShotPrefab, tail.position, Quaternion.Euler(new Vector3(0, -180, 180)));
            tmp1.GetComponent<Boss_TailShot>().Initialize(Vector2.left);
            myAnimator.SetTrigger("Tail");
            tmp2 = (GameObject)Instantiate(doubletailShotPrefab, tail2.position, Quaternion.Euler(new Vector3(0, 0, -180)));
            tmp2.GetComponent<Boss_TailShot>().Initialize(Vector2.right);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!invincible)
        {
            if (other.tag == "SupergirlFoot")
            {
                //Debug.Log("Trigger");
				GetComponent<AudioSource>().PlayOneShot(hitSound, 0.8f);
                invincible = true;
                StartCoroutine(Flash(flashSpeed));
                StartCoroutine(BeginTimeout());
                StartCoroutine(TakeDamage(10));
            }
            if (other.tag == "Laser")
            {
                invincible = true;
                StartCoroutine(Flash(flashSpeed));
                StartCoroutine(BeginTimeout());
                StartCoroutine(TakeDamage(10));
            }
        }
        if (other.tag == "Edge")
        {
            ChangeDirection(-1, 0.4F);
        }
    }

    private IEnumerator BeginTimeout()
    {
        yield return new WaitForSeconds(invincible_time);
        invincible = false;
    }

    private IEnumerator Flash(float x)
    {
        //Debug.Log("Flashing");
        for (int i = 0; i < 20; i++)
        {
            spRndrer.enabled = false;
            yield return new WaitForSeconds(x);
            spRndrer.enabled = true;
            yield return new WaitForSeconds(x);
        }
    }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        if (!GetIsDead())
        {
			GetComponent<AudioSource>().PlayOneShot(damageSound, 0.8f);
            enraged = true;
        }
        else
        {
            BossDeath();
            yield return null;
        }
    }

    protected void BossDeath()
    {
        moveSpeed = 0; //Stops the monster from moving when it dies
        myAnimator.SetTrigger("Die"); //Triggers the animation for death 
        GetComponent<BoxCollider2D>().enabled = false;
        myRigidBody.gravityScale = 0;
        StartCoroutine(onCompletion());
    }

    IEnumerator onCompletion()
    {

        while (myAnimator.GetBool("deathWord"))
        {
            //Debug.Log("Animation in progress!");
            yield return null;
        }
        Destroy(gameObject);
        //Debug.Log("Animation complete!");
    }
}
