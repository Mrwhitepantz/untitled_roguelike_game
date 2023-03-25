using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Using
    public TopDownController movement;
    public ShootingController shooter;
    public CamShake cineCam;

    //Yet to use
    public PlayerHealth health;
    //public ItemManager inventory;

    public Vector2 direction;
    public Rigidbody2D body;
    public bool Test = false;
    public Vector2 testDirection;
    void Start()
    {
        //controller = new TopDownController("Squirtle"); // this unfortunately does not work. Get a NullReference exception
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<TopDownController>(); // this is the equivalent of going to inspector tab and providing a game object
        shooter = GetComponent<ShootingController>();
        health = GetComponent<PlayerHealth>();
        cineCam = GetComponent<CamShake>();
    }

    //Any code that ISN'T updating with rigidbody2D goes here
    void Update()
    {
        if (Test == false){
            direction = movement.getInput();
        } else {
            direction = testDirection;
        }

        //movement.animate(animator, direction);
        movement.animate(direction);
        //Debug.Log("Mouse position: " + shooter.lookAtMouse(body.position));
    }
    //Any code that IS updating any rigidBody values  goes here
    void FixedUpdate()
    {
        if (Input.GetKey("mouse 1") && movement.canDash())
        {
            Debug.Log("dash");
            StartCoroutine(movement.dash(body, direction)); //you can pass the body, update it's velocity in a different class
            cineCam.shakeCamera(5f, .1f); //causing some null reference exceptions
        }
        body.velocity = movement.run(body.velocity, direction);
        //body.rotation = shooter.lookAtMouse(body.position);
    }

}
