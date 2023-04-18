using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingController : MonoBehaviour
{
    [SerializeField] public Gun gun; //script of the current weapon
    [SerializeField] public bool hasWeapon;
    [SerializeField] protected Camera sceneCam;

    void Start()
    {
        sceneCam = Camera.main;
        hasWeapon = false;
        gun = null;
    }

    //Checks if user presses left click, if yes then shoot bullets
    void Update()
    {
        //Zach: Citation - https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=123s
        if (Input.GetButton("Fire1") && hasWeapon)
        {
            gun.shoot();
            /*if (gun.Equals("M1Garand"))
            {
                gun.decrementClip(); //doesn't work
            }*/
        }
        //Debug.Log("mouse position" + Input.mousePosition);
    }

    //Rotates weapon sprite based on mouse location
    public float lookAtMouse(Vector2 playerPos)
    {
        Vector2 mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - playerPos;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        return aimAngle + 180;
    }
}
