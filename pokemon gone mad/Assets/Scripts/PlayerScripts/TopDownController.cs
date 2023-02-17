using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    //Ideally make these variables private but idk who else have been accessing these fields
    //Variables related to movement
    public float maxSpeed = 8f;
    public float maxAccel = 95f;
    public float friction = 1f;
    public Vector2 desiredVelocity;
    public float maxSpeedChange;
    public bool pauseState;

    //Variables for dashing
    private float dashSpeed = 50f;
    private float dashDuration = .15f;
    private float dashCooldown = .95f;
    private bool dashCounter = true;

    public bool canDash()
    {
        return dashCounter;
    }

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
        desiredVelocity = new Vector2(inputDir.x, inputDir.y) * (maxSpeed - friction);
        currVelocity.x = Mathf.MoveTowards(currVelocity.x, desiredVelocity.x, maxSpeedChange);
        currVelocity.y = Mathf.MoveTowards(currVelocity.y, desiredVelocity.y, maxSpeedChange);
        return currVelocity;
    }

    // Get's x and y values
    public Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void animate(Animator animator, Vector2 inputDir)
    {
        if (inputDir.x != 0 || inputDir.y != 0)
        {
            if (inputDir.x > 0)
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