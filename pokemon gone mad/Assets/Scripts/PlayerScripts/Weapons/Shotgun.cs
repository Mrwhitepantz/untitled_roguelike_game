using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shoots 3 projectiles in a spread arc
public class Shotgun : Gun
{
    //Inherits the following from Gun base class
        //Transform firePoint;       //where the bullets will appear
        //GameObject bulletPrefab;   //the bullet asset it will shoot, includes sprite, rigidbody2d, and the collider
        //Rigidbody2D weapon;        //the body of the gun for physics
        //AudioClip gunshotSFX;      //audio for gunshot
        //AudioClip equipSFX;        //audio for equipping the gun
        //AudioSource sfx;           //master audio component
        //GameObject impactEffect;   //aesthetic effect when bullet hits something
        //LineRenderer lineRenderer; //renders a line from one point to another
    [SerializeField] protected Transform firePoint2;
    [SerializeField] protected Transform firePoint3;
    [SerializeField] protected float fireRate = .65f;
    [SerializeField] protected float bulletSpeed = 20f;

    public override void shoot()
    {
        StartCoroutine("shotDelay", fireRate);
    }

    private IEnumerator shotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject projectile1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject projectile2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        GameObject projectile3 = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        Rigidbody2D bullet1 = projectile1.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet2 = projectile2.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet3 = projectile3.GetComponent<Rigidbody2D>();
        bullet1.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        bullet2.AddForce(firePoint2.up * bulletSpeed, ForceMode2D.Impulse);
        bullet3.AddForce(firePoint2.up * bulletSpeed, ForceMode2D.Impulse);
        sfx.PlayOneShot(gunshotSFX, .3f);
        StopCoroutine("shotDelay");
    }

    public override void decrementClip()
    {

    }
    public override void reload()
    {

    }
    public override int getAmmoCount()
    {
        return 0;
    }
}
