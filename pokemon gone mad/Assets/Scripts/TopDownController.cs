using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D body;

    //Variables related to movement
    public float maxSpeed;
    public float linearDrag;
    public float acceleration;

    //private Vector2 direction;
    private float X;
    private float Y;
    private bool debug;

    // Start is called before the first frame update
    // Initialize starting values here (ex. character speed, character direction)
    void Start()
    {
        linearDrag = 10f;
        acceleration = 9.81f;
        maxSpeed = 5f;
        debug = true;
    }

    // Update is called once per frame
    // Code that affects getting input values
    void Update()
    {
        // X & Y is 0 when nothing is pressed
        // X is 1 when moving right, -1 when moving left
        // Y is 1 when moving up, -1 when moving down
        X = getInput().x;
        Y = getInput().y;
        if (debug)
        {
            Debug.Log("X: " + X + ", " + "Y: " + Y);
        }
        //Jame's code
        //direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized; //responsible for player movement
        //body.velocity = direction * moveSpeed;
        /*if(direction.x != 0 || direction.y != 0)
        {
            animator.SetFloat("speed", 1);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }*/
    }

    // Code that affects rigid body physics
    void FixedUpdate()
    {
        movePlayerIcePhysics();
    }

    private void movePlayer()
    {

    }
    // Moves the player
    private void movePlayerIcePhysics()
    {
        float targetSpeed = Mathf.Lerp(X, maxSpeed, 2f);
        float speedDif = targetSpeed - X;
        float movement = speedDif * acceleration;
        body.AddForce(movement * new Vector2(X, Y), ForceMode2D.Force);
        //body.velocity = new Vector2(X * moveSpeed, Y * moveSpeed);
    }

    // Get's x and y values
    private Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
