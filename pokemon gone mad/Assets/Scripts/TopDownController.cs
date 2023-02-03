using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D body;

    //Variables related to movement
    public float moveSpeed;
    public float maxSpeed;
    public float linearDrag;
    //private Vector2 direction;
    private float X;
    private float Y;

    // Start is called before the first frame update
    // Initialize starting values here (ex. character speed, character direction)
    void Start()
    {
        moveSpeed = 5f;
        linearDrag = 10f;
    }

    // Update is called once per frame
    // Code that affects getting input values
    void Update()
    {
        X = getInput().x;
        Y = getInput().y;
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
        //body.MovePosition(body.position + direction * moveSpeed * Time.fixedDeltaTime);
        movePlayer();
        if (Mathf.Abs(body.velocity.x) > maxSpeed)
        {
            
        }
    }

    // Moves the player
    private void movePlayer()
    {
        body.velocity = new Vector2(X * moveSpeed, Y * moveSpeed);
    }

    // Get's x and y values
    private Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
