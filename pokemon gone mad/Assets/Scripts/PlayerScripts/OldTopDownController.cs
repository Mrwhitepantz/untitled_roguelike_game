using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* This is the old topdowncontroller. This does not interface with Player script
 */
public class OldTopDownController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D body;
    public Camera sceneCam;
    //public Weapon weapon; // commented this out because it will lead to compile error

    private bool debug;
    //Make these public if we want to adjust any fields while in playground mode (but turn it into private after finishing)
    //Variables related to movement
    public float maxSpeed = 8f;
    public float maxAccel = 95f;
    public float friction = 1f;
    public Vector2 direction, desiredVelocity, currVelocity;
    public float maxSpeedChange;
    public bool pauseState;

    //Variables for dashing
    private float dashSpeed = 50f;
    private float dashDuration = .25f;
    private float dashCooldown = .75f;
    private bool canDash = true;

    private string name;

    public OldTopDownController(string name)
    {
        this.name = name;
        /*this.maxSpeed = 8f;
        this.maxAccel = 95f;
        this.friction = 1f;*/
    }

    // Start is called before the first frame update
    // Place fields here if you want to edit them while in playground mode
    void Start()
    {
        //sceneCam = GetComponent<Camera>();
        //weapon = GetComponent<Weapon>();
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
    // Code that affects getting input values for movement
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
        if (pauseState)
        {
            body.velocity = Vector2.zero; //ideally should have this in FixedUpdate
        }
        else
        {
            // X & Y is 0 when nothing is pressed
            // direction.x is 1 when moving right, -1 when moving left
            // direction.y is 1 when moving up, -1 when moving down
            direction.x = getInput().x;
            direction.y = getInput().y;
            desiredVelocity = new Vector2(direction.x, direction.y) * (maxSpeed - friction);
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
        //naiveRun();
        //runIcePhysics();
    }

    private void lookAtMouse()
    {
        Vector2 mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - body.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        body.rotation = aimAngle;
    }

    public bool checkDash()
    {
        return canDash;
    }

    // Ideal implementation of dashing
    public IEnumerator dash()
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

    //Overloaded version of run for player class
    /*public Vector2 run(Vector2 currVelocity, Vector2 inputDir)
    {
        float maxSpeedChange = 95f * Time.deltaTime;
        Vector2 desiredVelocity = new Vector2(inputDir.x, inputDir.y) * (8f - 1f);
        currVelocity.x = Mathf.MoveTowards(currVelocity.x, desiredVelocity.x, maxSpeedChange);
        currVelocity.y = Mathf.MoveTowards(currVelocity.y, desiredVelocity.y, maxSpeedChange);
        return currVelocity;
    }*/

    //Overloaded version of run for player class
    public Vector2 run(Vector2 currVelocity, Vector2 inputDir)
    {
        float maxSpeedChange = maxAccel * Time.deltaTime;
        Vector2 desiredVelocity = new Vector2(inputDir.x, inputDir.y) * (maxSpeed - friction);
        currVelocity.x = Mathf.MoveTowards(currVelocity.x, desiredVelocity.x, maxSpeedChange);
        currVelocity.y = Mathf.MoveTowards(currVelocity.y, desiredVelocity.y, maxSpeedChange);
        return currVelocity;
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
        float targetSpeed = Mathf.Lerp(direction.x, maxSpeed, .5f);
        float speedDif = targetSpeed - direction.x;
        float movement = speedDif * (maxAccel-80);
        body.AddForce(movement * new Vector2(direction.x, direction.y), ForceMode2D.Force);
    }

    // Get's x and y values
    public Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}