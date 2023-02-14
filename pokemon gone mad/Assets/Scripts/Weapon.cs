using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float bulletForce;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fire();
        }
    }
    
    public void fire()
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
