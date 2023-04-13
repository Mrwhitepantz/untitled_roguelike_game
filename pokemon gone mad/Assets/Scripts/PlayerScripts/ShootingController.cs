using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] protected Gun weapon;
    [SerializeField] protected Camera sceneCam;

    public void Start()
    {
        sceneCam = Camera.main;
        //weapon = GetComponent<PistolRaycast>(); //will need to create some kind of weapon finder script
        //gunPoint = GetComponent<Transform>();
        //bulletPrefab = GetComponent<pBullet>();
        //bulletScript = GetComponent<playerBullet>();
        //bullet = GetComponent<Rigidbody2D>();
        //bulletTrail = GetComponent<BulletTrail2>();
    }

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            weapon.shoot();
            //weapon.shootRaycast();
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
}
