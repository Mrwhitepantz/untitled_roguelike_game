using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] public Gun gun; //script of the weapon
    [SerializeField] protected Camera sceneCam;
    //[SerializeField] protected playerBullet bulletPrefab;
    public bool hasWeapon;

    void Start()
    {
        sceneCam = Camera.main;
        hasWeapon = false;
        gun = null;
        //bulletPrefab = null;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && hasWeapon)
        {
            gun.shoot();
        }
        //Debug.Log("mouse position" + Input.mousePosition);
    }

    public float lookAtMouse(Vector2 playerPos)
    {
        Vector2 mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - playerPos;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        return aimAngle + 180;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

    }

}
