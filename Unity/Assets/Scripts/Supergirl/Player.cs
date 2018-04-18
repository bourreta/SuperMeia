using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	//Jumping variables
	[SerializeField]
	[Range(1, 15)]
	public float jumpForce; //Force of the jump.
	[SerializeField]
	private float fallMultiplier = 2.5f;
	[SerializeField]
	private float lowJumpMultiplier = 2f;
	[SerializeField]
	private Transform[] groundPoints; //Used to determine if the player character is grounded for the use of jumping
	[SerializeField]
	private float groundRadius; //Size of the groundpoints above
	[SerializeField]
	private LayerMask whatIsGround; //What the groundPoints consider ground to be able to jump off of
	private bool isGrounded; //Determines if the character is on the ground
	private int touchingGround; 
	private bool jump; //Determines if the character is currently in the middle of a jump
	[SerializeField]
	public bool airControl; //Allows the character to be able to control movement while off the ground. If off character can't control their jumps. 
	public bool isPoweredUp;

	[SerializeField]
	private GameObject laserPrefab;
	[SerializeField]
	public Transform eye;
	[SerializeField]
	private float rateOfFire;
	private float lastShot;
	[SerializeField]
	private float knockback;
	[SerializeField]
	private float knockbackLength;
	[SerializeField]
	private bool isknockbacked;

	[SerializeField]
	private float deadDrop;

	private int damage;

	[SerializeField]
	private float invincible_time;
	private bool killedByTime = false;
	public float flashSpeed;
	SpriteRenderer spRndrer;
	public AudioClip jumpSound;
	public AudioClip damageSound;
	public AudioClip deathSound;
	private TouchControls touchControls;
	//for managing death/respawn/checkpoints
	private LevelManager levelManager;
	private Vector3 positionOnDeath;
	// Use this for initialization
	public override void Start ()
	{
		base.Start();
		facingRight = true;
		spRndrer = GetComponent<SpriteRenderer>();
		levelManager = FindObjectOfType<LevelManager> ();
		touchControls = FindObjectOfType<TouchControls> ();

	}

	private void Update()
	{
		HandleInput();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{

		if (GetIsDead() == true && positionOnDeath.y - transform.position.y < 0.5){ 
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1); 
			return; 
		}else if (GetIsDead()){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			return;
		}

		//float horizontal = Input.GetAxis("Horizontal");
		float horizontal = 0;
		isGrounded = IsGrounded();
		Movement(horizontal);
		//Improves the feel of the jump
		if (myRigidBody.velocity.y < 0){ 
			myRigidBody.gravityScale = fallMultiplier; 
		}
		//else if (myRigidBody.velocity.y > 0 && (!Input.GetButton("Jump") && !Input.GetKey("up"))){ 
		else if (myRigidBody.velocity.y > 0 && (!touchControls.jumpButtonDown && !Input.GetButton("Jump") && !Input.GetKey("up"))){ 
			myRigidBody.gravityScale = lowJumpMultiplier; 
		}
		else { 
			myRigidBody.gravityScale = 1; 
		}

		HandleLayers();
		ResetValues();
	}

	void LateUpdate(){
		//Kill player if time expires
		if (TimeTracker.getTimeAsFloat () <= 0 && !GetIsDead() && !killedByTime) {
			killedByTime = true;
			Invoke ("resetKilledByTime", 6);
			StartCoroutine(TakeDamage(100));
		}
	}
	
	private void resetKilledByTime(){
		killedByTime = false;
	}
	//Handles the movement of the player.
	private void Movement(float horizontal)
	{
		//Sets the jump animation to falling
		if(myRigidBody.velocity.y < -0.1)
		{
			myAnimator.SetBool("land", true);
		}

		if(isGrounded || airControl)
		{
			//myRigidBody.velocity = new Vector2(horizontal * moveSpeed, myRigidBody.velocity.y); //Moves character left and right. 
			//myAnimator.SetFloat("speed", Mathf.Abs(horizontal)); //Sets the animators speed to change animation.
#if UNITY_EDITOR
			Move(Input.GetAxisRaw("Horizontal"));
#endif
		}
		//Jumping
		if(isGrounded && jump)
		{
			isGrounded = false;
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			myAnimator.SetTrigger("jump");
			float originalVol = GetComponent<AudioSource>().volume;
			GetComponent<AudioSource>().clip = jumpSound;
			GetComponent<AudioSource> ().volume = 0.2f;
			GetComponent<AudioSource>().Play();
			GetComponent<AudioSource> ().volume = originalVol;
		}
		if (myRigidBody.velocity.y > 0.1 && !isGrounded)
		{
			myAnimator.SetTrigger("jump"); 
		}
	}

	//Flips the character from right to left based on movement.
	public void Flip(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
		{
			ChangeDirection(-1, 1);
		}
	}

	//Handles input from the player
	private void HandleInput(){

		if(Input.GetButtonDown("Jump") || Input.GetKeyDown("up"))
		{
			Jump ();
		}

		if((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.DownArrow)))
		{
			Shoot ();
		}
	}

	public void Move(float input){
		myRigidBody.velocity = new Vector2(input * moveSpeed, myRigidBody.velocity.y); //Moves character left and right. 
		myAnimator.SetFloat("speed", Mathf.Abs(input)); //Sets the animators speed to change animation.
		Flip(input);
	}

	public void Shoot(){
		if(Time.time >= lastShot && !GetIsDead() && isPoweredUp)
		{
			lastShot = Time.time + rateOfFire;
			ShootLaser(0);
		}
	}

	public void Jump(){
		jump = true;
	}

	//Checks if Player is on the ground for jumping
	private bool IsGrounded()
	{
		if(myRigidBody.velocity.y <= 0)
		{
			foreach(Transform point in groundPoints)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
				for(int i = 0; i < colliders.Length; i++)
				{
					if(colliders[i].gameObject != gameObject)
					{
						touchingGround++;
					}
					if (touchingGround != 0)
					{
						isknockbacked = false;
						myAnimator.ResetTrigger("jump");
						myAnimator.SetBool("land", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	private void ResetValues()
	{
		jump = false;
	}

	private void HandleLayers()
	{
		if (!isGrounded && !isPoweredUp)
		{
			myAnimator.SetLayerWeight(1, 1);    //Jump Layer
		}
		else if (isPoweredUp && !isGrounded)
		{
			myAnimator.SetLayerWeight(3, 1);    //Powered Up & in the air
		}
		else if (isPoweredUp)
		{
			myAnimator.SetLayerWeight(2, 1);    //Powred up
			myAnimator.SetLayerWeight(3, 0);    //
		}
		else
		{
			myAnimator.SetLayerWeight(1, 0);    //Grounded not powered up
			myAnimator.SetLayerWeight(3, 0);
		}
	}

	public void ShootLaser(int value)
	{
		GameObject tmp;

		if(facingRight)
		{
			tmp = (GameObject)Instantiate(laserPrefab, eye.position, Quaternion.identity);
			tmp.GetComponent<Laser>().Initialize(Vector2.right);
		}
		else
		{
			tmp = (GameObject)Instantiate(laserPrefab, eye.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			tmp.GetComponent<Laser>().Initialize(Vector2.left);
		}

	}

	public IEnumerator TakeDamage(int damage)
	{
		health -= damage;
		if(!GetIsDead() && !isknockbacked)
		{
			// myAnimator.SetTrigger("damage");
			float originalVol = GetComponent<AudioSource>().volume;
			GetComponent<AudioSource>().clip = damageSound;
			GetComponent<AudioSource> ().volume = 1f;
			GetComponent<AudioSource>().Play();
			GetComponent<AudioSource> ().volume = originalVol;
			isknockbacked = true;
			if (facingRight)
			{
				myRigidBody.velocity = new Vector2 (-5, 10);
				//myRigidBody.AddForce(new Vector2(-1000, 0), ForceMode2D.Impulse);
			}
			if (!facingRight)
			{
				myRigidBody.velocity = new Vector2 (5, 10);
				//myRigidBody.AddForce(transform.up * knockback, ForceMode2D.Impulse);

			}
		}
		else if(GetIsDead())
		{
			positionOnDeath = transform.position;
			myAnimator.SetTrigger ("die");
			GetComponent<AudioSource> ().clip = deathSound;
			GetComponent<AudioSource> ().Play ();
			GetComponent<BoxCollider2D> ().enabled = false;
			myRigidBody.gravityScale = 0;
			myRigidBody.velocity = Vector2.one;
			StartCoroutine ("DieAndRespawn");
			yield return null;
		}
	}

	public virtual void OnCollisionEnter2D(Collision2D other)
	{
		if (!invincible)
		{	//
			if (other.gameObject.tag == "Enemy" && !(gameObject.transform.position.y > other.gameObject.transform.position.y + 1))
			{
				damage = 25;
				this.transform.GetComponentInChildren<KillZoneFeet>().stomper = false;
				invincible = true;
				airControl = false;
				StartCoroutine(NoStomping());
				StartCoroutine(Flash(flashSpeed));
				StartCoroutine(BeginTimeout());
				StartCoroutine(TakeDamage(damage));
			}
		}
		if (other.gameObject.tag == "FloorOfLevel")
		{
			damage = 100;
			StartCoroutine(TakeDamage(damage));
		}
		if (other.transform.tag == "Moving Platform") {
			transform.parent = other.transform;
		}
	}

	public void OnCollisionExit2D(Collision2D other){
		if (other.transform.tag == "Moving Platform") {
			this.transform.parent = null;
		}
	}

	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (!invincible)
		{
			if (other.gameObject.tag == "Enemy")
			{
				damage = 25;
				this.transform.GetComponentInChildren<KillZoneFeet>().stomper = false;
				invincible = true;
				StartCoroutine(NoStomping());
				StartCoroutine(Flash(flashSpeed));
				StartCoroutine(BeginTimeout());
				StartCoroutine(TakeDamage(damage));
			}
		}
		if (other.tag == "Star")
		{
			isPoweredUp = true;
		}
		else if (other.tag == "Health")
		{

			health = 100;
		}
	}

	private IEnumerator DieAndRespawn(){
		yield return new WaitForSeconds (4);

		isPoweredUp = false;
		LifeTracker.takeLife ();

		if (LifeTracker.getLives () > 0) {
			if (!facingRight) {
				ChangeDirection (-1, 1);
			}
			GetComponent<BoxCollider2D>().enabled = true; 

			levelManager.RespawnPlayer ();
			myAnimator.Rebind ();
			health = 100;
		}
	}

	private IEnumerator BeginTimeout()
	{
		yield return new WaitForSeconds(invincible_time);
		invincible = false;
	}

	private IEnumerator Flash(float x)
	{
		for (int i = 0; i < 10; i++)
		{
			spRndrer.enabled = false;
			yield return new WaitForSeconds(x);
			spRndrer.enabled = true;
			yield return new WaitForSeconds(x);
		}
	}

	private IEnumerator NoStomping()
	{
		yield return new WaitForSeconds(0.4F);
		airControl = true;
#if UNITY_IOS || UNITY_ANDROID
		Move(0);
#endif
		this.transform.GetComponentInChildren<KillZoneFeet>().stomper = true;
	}


}
