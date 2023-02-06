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
    public float maxSpeed;
    public float maxAccel;
    //public float maxDecel = 50f;
    public float friction = 1f;
    private Vector2 direction, desiredVelocity, currVelocity;
    private float maxSpeedChange, acceleration;
    private bool debug;

    // Start is called before the first frame update
    // Place fields here if you want to edit them while in playground mode
    void Start()
    {
        debug = true;
        maxSpeed = 8f;
        maxAccel = 95f;
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
        float timer = Time.time;
        if (debug)
        {
            //Debug.Log("X: " + direction.x + ", " + "Y: " + direction.y);
            //Debug.Log("timer: " + timer);
        }
        //Jame's code
        if (direction.x != 0 || direction.y != 0)
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
        if (Input.GetKey("mouse 1")) //left click
        {
            Debug.Log("Dash!");
            dash();
        }
        movePlayer();
        //movePlayerBasic();
        //movePlayerIcePhysics();
    }

    private void dash()
    {
        float dashDuration = 1f;
        float start = Time.time;

        while (dashDuration >= 0)
        {
            body.velocity = direction * 100;
            dashDuration -= Time.deltaTime;
            Debug.Log(dashDuration);
        }
        body.velocity = direction * maxSpeed;
        //body.velocity = direction * 100;
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

    // Barebones version of player movement
    private void movePlayerBasic()
    {
        //X * maxSpeed = 1 * maxSpeed
        body.velocity = new Vector2(direction.x * maxSpeed, direction.y * maxSpeed);
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
