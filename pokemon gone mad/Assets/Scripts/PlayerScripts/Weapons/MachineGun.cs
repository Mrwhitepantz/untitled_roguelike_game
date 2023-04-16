using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
{
    //Inherits the following from Gun base class
        //public Transform firePoint; //where the bullets will appear
        //public GameObject bulletPrefab; //the bullet asset it will shoot, includes sprite, rigidbody2d, and the collider
        //public playerBullet bulletScript; //script corresponding to bullet

        //public GameObject impactEffect;
        //public LineRenderer lineRenderer;

    [SerializeField] protected float fireRate = .15f;


    public override void shoot()
    {
        StartCoroutine("shotDelay", fireRate);
        //gunshotSFX.Play();
    }

    private IEnumerator shotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bullet = projectile.GetComponent<Rigidbody2D>();
        bullet.AddForce(firePoint.up * bulletScript.speed, ForceMode2D.Impulse);
        //may need to call OnTrigger2D when implementing damage
        
        StopCoroutine("shotDelay");

    }
}
