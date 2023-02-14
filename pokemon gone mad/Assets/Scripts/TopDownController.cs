using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D body;
    public Camera sceneCam;
    public Weapon weapon;

    private bool debug;
    //Make these public if we want to adjust any fields while in playground mode (but turn it into private after finishing)
    //Variables related to movement
    private float maxSpeed;
    private float maxAccel;
    private float friction;
    private Vector2 direction, desiredVelocity, currVelocity;
    private float maxSpeedChange;
    private bool pauseState;

    //Variables for dashing (turn them into private after testing)
    private float dashSpeed;
    private float dashDuration;
    private float dashCooldown;
    private bool canDash;

    // Start is called before the first frame update
    // Place fields here if you want to edit them while in playground mode
    void Start()
    {
        debug = true;

        //Movement
        maxSpeed = 8f;
        maxAccel = 95f;
        friction = 1f;
        pauseState = false;

        //Dashing
        dashDuration = .25f;
        dashCooldown = .75f;
        dashSpeed = 50f;
        canDash = true;
    }

    // Update is called once per frame
    // Code that affects getting input values
    void Update()
    {
        // X & Y = 0 when nothing is pressed
        // direction.x is 1 when moving right, -1 when moving left
        // direction.y is 1 when moving up, -1 when moving down
        direction = getInput();
        desiredVelocity = new Vector2(direction.x, direction.y) * (maxSpeed - friction);
        lookAtMouse();
        //Jame's code
        if (direction.x != 0 || direction.y != 0)
        {
            if (direction.x > 0)
            {
                animator.SetFloat("horizontal", 1);
            }
            else
            {
                animator.SetFloat("horizontal", -1);
            }
            animator.SetFloat("speed", 1);
        }
        else
        {
            animator.SetFloat("horizontal", 0);
            animator.SetFloat("speed", 0);
        }
    }

    // Code that affects rigid body physics
    void FixedUpdate()
    {
        if (Input.GetKey("mouse 1") && canDash) //right click
        {
            StartCoroutine(dash()); //personally prefer this one
            //naiveDash();
        }
        run();
        //runNaive();
        //runIcePhysics();
    }

    private void lookAtMouse()
    {
        Vector2 mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);
        if (debug)
        {
            //Debug.Log("Mouse X: " + mousePos.x + " Y: " + mousePos.y);
            //Vector2 direction = controller.getCurrVelocity();
            //Debug.Log("Player X: " + direction.x + " Y: " + direction.y);
            //float aimAngle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg - 90f;
        }

        Vector2 aimDirection = mousePos - body.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        body.rotation = aimAngle;
    }

    // Ideal implementation of dashing
    private IEnumerator dash()
    {
        canDash = false;
        body.velocity = direction * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        body.velocity = new Vector2(direction.x * (maxSpeed / 1.5f), direction.y * (maxSpeed / 1.5f)); // higher the divisor, the choppier end of dash feels
        //body.velocity = new Vector2(direction.x * (maxSpeed / 2f), direction.y * (maxSpeed / 2f));
        //body.velocity = direction; // doesn't feel as good
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void naiveDash()
    {
        body.velocity = direction * dashSpeed;
    }

    // Ideal implementation of player movement
    /*  Higher maxSpeed + lower maxAccel makes character feel more weighty
     *  Lower maxSpeed + higher maxAccel makes character feel closer to basic movement
     *  Friction represents the type of ground character is moving in. Higher friction = generally slower movement speed
     */
    private void run()
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
    }

    // Barebones version of player movement
    private void naiveRun()
    {
        //X * maxSpeed = 1 * maxSpeed
        body.velocity = new Vector2(direction.x * maxSpeed, direction.y * maxSpeed);
    }

    // Ice physics
    private void runIcePhysics()
    {
        float targetSpeed = Mathf.Lerp(direction.x, maxSpeed, 2f);
        float speedDif = targetSpeed - direction.x;
        float movement = speedDif * maxAccel;
        body.AddForce(movement * new Vector2(direction.x, direction.y), ForceMode2D.Force);
    }

    // Get's x and y values
    private Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}