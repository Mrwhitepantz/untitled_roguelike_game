using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consider making this an abstract class
public class Weapon : MonoBehaviour
{
    public Transform firePoint; //where the bullets will appear
    public GameObject bulletPrefab; //the bullet asset it will shoot, includes sprite, rigidbody2d, and the collider
    public playerBullet bulletScript; //script corresponding to bullet
    
    /*  projectile is the bullet prefab
     *  bullet is the rigidbody2d
     *  is using bulletscript's speed
     */
    public void shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bullet = projectile.GetComponent<Rigidbody2D>();
        bullet.AddForce(firePoint.up * bulletScript.speed, ForceMode2D.Impulse);
        Destroy(projectile, 1);
    }
}
