using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Using
    public TopDownController movement;
    public ShootingController shooter;
    public CamShake cineCam;

    public GameObject gun;
    public Rigidbody2D gunBody;
    public bool hasWeapon;

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
        gun = null; // this will have to involve collision
        hasWeapon = false;
        gunBody = null;
        //cineCam = GetComponent<CamShake>();
    }

    //Zach: Any code that ISN'T updating with rigidbody2D goes here
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
    //Zach: Any code that IS updating any rigidBody values  goes here
    void FixedUpdate()
    {
        if (Input.GetKey("mouse 1") && movement.canDash())
        {
            Debug.Log("dash");
            StartCoroutine(movement.dash(body, direction)); //you can pass the body, update it's velocity in a different class
            StartCoroutine(movement.dash(gunBody, direction));
            //cineCam.shakeCamera(5f, .1f); //causing some null reference exceptions
        }
        body.velocity = movement.run(body.velocity, direction);
        if (hasWeapon)
        {
            gunBody.velocity = movement.run(gunBody.velocity, direction);
            gunBody.rotation = shooter.lookAtMouse(gunBody.position);
        }

        //COMMENTED OUT TO DISABLE ROTATION

        //gunRotationNoah(body.velocity, gunBody);
        //gunRotationZach(gunBody, body);
    }

    public void gunRotationZach(Rigidbody2D target, Rigidbody2D player)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - player.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        target.transform.RotateAround(player.position, new Vector3(0, 0, 1), aimAngle);
    }

    public void gunRotationNoah(Vector2 target, Rigidbody2D rb)
    {
        Vector2 dirction = ((Vector2)target - rb.position).normalized;
        //Vector2 force = dirction * speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //rb.AddForce(force);
        float angle = target.y / target.x;
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        if (target.x < 0)
        {
            if (target.y > 0)
            {
                angle = target.y / target.x;
                angle = Mathf.Rad2Deg * Mathf.Atan(angle);
            }
        }
        if (target.x > 0)
        {
            angle = target.y / target.x;
            angle = Mathf.Rad2Deg * Mathf.Atan(angle) + 180;
        }
        rb.rotation = angle;
        //Debug.Log(rb.rotation);
        //transform.position += target * 90 * Time.deltaTime;
    }
}
