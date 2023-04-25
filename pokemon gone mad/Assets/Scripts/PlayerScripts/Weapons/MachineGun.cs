using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
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

    [SerializeField] protected float fireRate = .15f;
    [SerializeField] protected float bulletSpeed = 25f;

    public override void shoot()
    {
        StartCoroutine("shotDelay", fireRate);
    }

    private IEnumerator shotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bullet = projectile.GetComponent<Rigidbody2D>();
        bullet.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
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

    public override bool isReloading()
    {
        throw new System.NotImplementedException();
    }
}
