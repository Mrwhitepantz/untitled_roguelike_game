using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D body;

    //Make these public if we want to adjust any fields while in playground mode
    //Variables related to movement
    public float maxSpeed = 4f;
    public float maxAccel = 35f;
    public float friction = 1f;
    private Vector2 direction, desiredVelocity, currVelocity;
    private float maxSpeedChange, acceleration;
    private bool debug;

    // Start is called before the first frame update
    // Place fields here if you want to edit them while in playground mode
    void Start()
    {
        debug = true;
    }

    // Update is called once per frame
    // Code that affects getting input values
    void Update()
    {
        // X & Y is 0 when nothing is pressed
        // direction.x is 1 when moving right, -1 when moving left
        // direction.y is 1 when moving up, -1 when moving down
        direction.x = getInput().x;
        direction.y = getInput().y;
        desiredVelocity = new Vector2(direction.x, direction.y) * (maxSpeed - friction);
        if (debug)
        {
            Debug.Log("X: " + direction.x + ", " + "Y: " + direction.y);
        }
        //Jame's code
        //direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized; //responsible for player movement
        //body.velocity = direction * moveSpeed;
       if(direction.x != 0 || direction.y != 0)
        {
            animator.SetFloat("speed", 1);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }

    // Code that affects rigid body physics
    void FixedUpdate()
    {
        movePlayer();
        //movePlayerBasic();
        //movePlayerIcePhysics();
    }

    // Barebones version of player movement
    private void movePlayerBasic()
    {
        //X * maxSpeed = 1 * maxSpeed
        body.velocity = new Vector2(direction.x * maxSpeed, direction.y * maxSpeed);
    }

    // Ideal implementation of player movement
    /*  Higher maxSpeed + lower maxAccel makes character feel more weighty
     *  Lower maxSpeed + higher maxAccel makes character feel closer to basic movement
     *  Friction represents the type of ground character is moving in. Higher friction = generally slower movement speed
     */
    private void movePlayer()
    {
        /*  What's happening:
         *  - body.velocity is the speed of the rigidbody, which is the character's movement speed
         *  - first need to calculate the acceleration (or in physics, the change in speed)
         *  - Mathf.MoveTowards() is the magic that allows the accelerated movement to happen. 
         *    First parameter is current speed, second is the maximum speed, third is the rate of change
         *    We are doing that for both the x and y velocities
         *  - then we update the rigidbody's velocity to the newly calculated speed
         */
        currVelocity = body.velocity;
        maxSpeedChange = maxAccel * Time.deltaTime;
        currVelocity.x = Mathf.MoveTowards(currVelocity.x, desiredVelocity.x, maxSpeedChange);
        currVelocity.y = Mathf.MoveTowards(currVelocity.y, desiredVelocity.y, maxSpeedChange);
        body.velocity = currVelocity;
        /* Next thing to implement:
         * - Separate acceleration and deceleration curves. Currently acceleration and deceleration
         *   have the same rate of change, but good games allow both to be different. (Refer
         *   to acceleration curves shown in https://www.youtube.com/watch?v=yorTG9at90g)
         */
    }
    // Ice physics
    private void movePlayerIcePhysics()
    {
        float targetSpeed = Mathf.Lerp(direction.x, maxSpeed, 2f);
        float speedDif = targetSpeed - direction.x;
        float movement = speedDif * acceleration;
        body.AddForce(movement * new Vector2(direction.x, direction.y), ForceMode2D.Force);
    }

    // Get's x and y values
    private Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
