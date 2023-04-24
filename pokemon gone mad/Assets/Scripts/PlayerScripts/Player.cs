using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private TopDownController movement;
    private ShootingController shooter;

    //Zach: Fields for weapons
    public GameObject gun;
    public Rigidbody2D gunBody;
    public bool hasWeapon;
    
    //Zach: Fields for movement
    private Vector2 direction;
    private Rigidbody2D body;
    public bool Test = false;
    public Vector2 testDirection;

    //Yet to use
    public PlayerHealth health;
    //public ItemManager inventory;

    public float aimDirection;
    public float aimDirection2;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<TopDownController>();
        shooter = GetComponent<ShootingController>();
        health = GetComponent<PlayerHealth>();
        gun = null; 
        hasWeapon = false;
        gunBody = null;
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

    //Zach: Any code that IS updating any rigidBody values goes here
    void FixedUpdate()
    {
        if (Input.GetKey("mouse 1") && movement.canDash())
        {
            Debug.Log("dash");
            StartCoroutine(movement.dash(body, direction)); //you can pass the body, update it's velocity in a different class
            if (hasWeapon)
            {
                StartCoroutine(movement.dash(gunBody, direction));
            }
        }
        body.velocity = movement.run(body.velocity, direction);
        if (hasWeapon)
        {
            //gunBody.velocity = movement.run(gunBody.velocity, direction); //Bug: if player collides with wall, gun still moves
            aimDirection = shooter.lookAtMouse(body.position);
            aimDirection2 = (aimDirection2) * Mathf.Deg2Rad;
            gunBody.rotation = aimDirection;
            aimDirection = (aimDirection + 180) * Mathf.Deg2Rad;
            gun.transform.position = gameObject.transform.position + new Vector3(Mathf.Cos(aimDirection), Mathf.Sin(aimDirection));
        }

        //COMMENTED OUT TO DISABLE ROTATION
        //gunRotationNoah(body.velocity, gunBody);
        //gunRotationZach(gunBody, body);
    }

    //Zach: Unused, was trying to figure out better way to rotate gun
    public void gunRotationZach(Rigidbody2D target, Rigidbody2D player)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - player.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        target.transform.RotateAround(player.position, new Vector3(0, 0, 1), aimAngle);
    }

    /*Zach: Noah's implementation of gun rotation. Has a bug where if player is moving up,
    gun points down, and if player is moving down, gun points up*/
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
