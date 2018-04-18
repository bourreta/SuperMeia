using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_TailShot : MonoBehaviour {
    [SerializeField]
    private float speed; //Speed of the projectile
    private Rigidbody2D myRigidBody;
    private Vector2 direction;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    //Destorys the laser if it collides with anything other than the pickups and Player.
    public void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag != "Platform" && other.tag != "Enemy" && other.tag != "star" && other.tag != "Health" && other.tag != "Star" && other.tag != "PowerUp" && other.tag != "CameraEdge" && other.tag != "Edge")
            Destroy(gameObject);
    }

    //Destorys the laser when it hits the edge of the camera
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CameraEdge")
            Destroy(gameObject);
    }

}
