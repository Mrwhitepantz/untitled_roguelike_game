using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingController : MonoBehaviour
{
    [SerializeField] public Gun gun; //script of the current weapon
    //[SerializeField] public GameObject gun;
    [SerializeField] public bool hasWeapon;
    [SerializeField] protected Camera sceneCam;
    [SerializeField] protected bool canShoot;

    void Start()
    {
        sceneCam = Camera.main;
        hasWeapon = false;
        gun = null;
        canShoot = true;
    }

    public bool couldShoot()
    {
        return canShoot;
    }

    //Checks if user presses left click, if yes then shoot bullets
    void Update()
    {
        /*Zach: Citation for setting up weapons
         * https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=123s*/
        if (gun is M1Garand && hasWeapon) //single action weapons
        {
            if (Input.GetMouseButtonDown(0) && hasWeapon && !gun.isReloading())
            {
                gun.shoot();
            }
            if (gun.getAmmoCount() == 0)
            {
                gun.reload();
            }
        } 
        else //automatic weapons
        {
            if (Input.GetMouseButton(0) && hasWeapon) {
                gun.shoot();
            }
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
