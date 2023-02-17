using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TopDownController controller;
    public Vector2 direction;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        //controller = new TopDownController("Squirtle"); // this unfortunately does not work. Get a NullReference exception
        controller = GetComponent<TopDownController>(); // this is the equivalent of going to inspector tab and providing a game object
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = controller.getInput();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("mouse 1") && controller.canDash())
        {
            Debug.Log("dash");
            StartCoroutine(controller.dash(body, direction)); //you can pass the body, update it's velocity in a different class
        }
        body.velocity = controller.run(body.velocity, direction);
    }
}
