using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TopDownController : MonoBehaviour
{
    //public GameObject player; //At first needed this, but for some reason I don't need to have it
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    //Ideally make these variables private but idk who else have been accessing these fields
    //Variables related to movement
    public float maxSpeed = 8f;
    public float maxAccel = 95f;
    public float friction = 1f;
    public Vector2 desiredVelocity;
    public float maxSpeedChange;
    public bool pauseState;
    public bool ifCollision = false;

    //Variables for directional movement
    public Vector2 directionTemp;

    //Variables for dashing
    private float dashSpeed = 50f;
    private float dashDuration = .15f;
    private float dashCooldown = .95f;
    private bool dashCounter = true;

    void Start()
    {
        //animator = player.GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }

    /* Helper method that checks if player can dash.
     * In the future - will implement dashCounter as an int so that dashes can be a renewable
     * resource
     */
    public bool canDash()
    {
        return dashCounter;
    }

    /* Performs a dash on character. After dashing for a certain amount of time, player will enter
     * a cooldown state.
     */
    //Bug: will use a dash when standing still
    public IEnumerator dash(Rigidbody2D body, Vector2 inputDir)
    {
        dashCounter = false;
        body.velocity = inputDir * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        body.velocity = new Vector2(inputDir.x * (maxSpeed / 1.25f), inputDir.y * (maxSpeed / 1.25f)); // higher the divisor, the choppier end of dash feels
        yield return new WaitForSeconds(dashCooldown);
        dashCounter = true;
    }

    //Overloaded version of run for player class
    public Vector2 run(Vector2 currVelocity, Vector2 inputDir)
    {
        maxSpeedChange = maxAccel * Time.deltaTime;
        desiredVelocity = new Vector2(inputDir.x, inputDir.y).normalized * (maxSpeed - friction);
        currVelocity.x = Mathf.MoveTowards(currVelocity.x, desiredVelocity.x, maxSpeedChange);
        currVelocity.y = Mathf.MoveTowards(currVelocity.y, desiredVelocity.y, maxSpeedChange);
        return currVelocity;
    }

    // Gets x and y values
    public Vector2 getInput()
    {
        directionTemp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //Debug.Log(directionTemp);
        return directionTemp; //needs to be normalized or else diagonal movement will go further
        //return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void animate(Vector2 inputDir)
    {
        
        //Debug.Log("x");
        //Debug.Log(inputDir.x);
        
        
        if (inputDir.x != 0 ){
            animator.SetFloat("speed", 1);
            if (inputDir.x > 0) {
                animator.SetFloat("horizontal", 1);
            } else { animator.SetFloat("horizontal", -1);}
        } else {animator.SetFloat("horizontal", 0);}
        if (inputDir.y != 0 ){
            //Debug.Log("y");
            //Debug.Log(inputDir.y);
            animator.SetFloat("speed", 1);
            if (inputDir.y > 0) {
                animator.SetFloat("vertical", 1);
            } else { animator.SetFloat("vertical", -1);}
            
        } else {animator.SetFloat("vertical", 0);}
        if ((inputDir.x == 0) && (inputDir.y == 0)) {
            
            animator.SetFloat("speed", 0);
        }
    }
}