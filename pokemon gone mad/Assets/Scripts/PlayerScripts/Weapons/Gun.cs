using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consider making this an abstract class
public abstract class Gun : MonoBehaviour
{
    public Transform firePoint; //where the bullets will appear
    public GameObject bulletPrefab; //the bullet asset it will shoot, includes sprite, rigidbody2d, and the collider
    public playerBullet bulletScript; //script corresponding to bullet
    public Rigidbody2D weapon;
    public AudioClip gunshotSFX;
    public AudioClip equipSFX;
    public AudioSource sfx;

    //public GameObject impactEffect;
    public LineRenderer lineRenderer;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

    }

    //[SerializeField] protected float fireRate; //for some reason can't create abstract variable?

    /*  projectile is the bullet prefab
     *  bullet is the rigidbody2d
     *  is using bulletscript's speed
     */
    public abstract void shoot();

    //public abstract IEnumerator shotDelay(float delay);
}
