using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shoots 1 fast projectile that can pierce through enemies
public class M1Garand : Gun
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
    [SerializeField] protected float fireRate = .5f;
    [SerializeField] protected float bulletSpeed = 70f;
    [SerializeField] protected int clipSize = 8;
    
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
        if (clipSize == 0)
        {
            sfx.Play(1);
        }
        StopCoroutine("shotDelay");
    }

    public override void decrementClip()
    {
        if (clipSize > 0)
        {
            clipSize = clipSize - 1;
        }
        Debug.Log("M1Garand clipsize = " + clipSize);
    }

    public override void reload()
    {
        StartCoroutine("reloadTime", 3f);
    }

    private IEnumerator reloadTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        clipSize = 8;
        StopCoroutine("reloadTime");
    }

    public override int getAmmoCount()
    {
        return clipSize;
    }

    
}
