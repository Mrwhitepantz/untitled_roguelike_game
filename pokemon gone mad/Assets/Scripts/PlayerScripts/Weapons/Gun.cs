using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consider making this an abstract class
public abstract class Gun : MonoBehaviour
{
    public Transform firePoint; //where the bullets will appear
    public GameObject bulletPrefab; //the bullet asset it will shoot, includes sprite, rigidbody2d, and the collider
    public playerBullet bulletScript; //script corresponding to bullet

    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    //public abstract float fireRate { get; set; } //Sadly I don't think this works

    /*  projectile is the bullet prefab
     *  bullet is the rigidbody2d
     *  is using bulletscript's speed
     */
    public abstract void shoot();
    
    // Raycast implemention of shooting
    public void shootRaycast()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.up), 10f);

        if (hitInfo)
        {
            Debug.Log("Hit something w/ raycast");
            //Instantiate(impactEffect, hitInfo.point, Quaternion.identity); //not sure what the Quaternion does
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            Debug.Log("Missed w/ raycast");
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, Input.mousePosition * 10);
        }
    }
}
