using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TopDownController movement;
    public ShootingController shooter;
    public Vector2 direction;
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //controller = new TopDownController("Squirtle"); // this unfortunately does not work. Get a NullReference exception
        movement = GetComponent<TopDownController>(); // this is the equivalent of going to inspector tab and providing a game object
        body = GetComponent<Rigidbody2D>();
        shooter = GetComponent<ShootingController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = movement.getInput();
        movement.animate(animator, direction);
        body.rotation = shooter.lookAtMouse(body.position);
    }

    void FixedUpdate()
    {
        if (Input.GetKey("mouse 1") && movement.canDash())
        {
            Debug.Log("dash");
            StartCoroutine(movement.dash(body, direction)); //you can pass the body, update it's velocity in a different class
        }
        body.velocity = movement.run(body.velocity, direction);
    }
}
