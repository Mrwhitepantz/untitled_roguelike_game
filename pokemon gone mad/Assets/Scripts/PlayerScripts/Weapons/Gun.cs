using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consider making this an abstract class
public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Transform firePoint; //where the bullets will appear
    [SerializeField] protected GameObject bulletPrefab; //the bullet asset it will shoot, includes sprite, rigidbody2d, and the collider
    //[SerializeField] protected playerBullet bulletScript; //script corresponding to bullet collision logic
    //[SerializeField] protected abstract float bulletSpeed { get; set; }
    //[SerializeField] public float bulletSpeed;
    [SerializeField] protected Rigidbody2D weapon;
    [SerializeField] protected AudioClip gunshotSFX;
    public AudioClip equipSFX;
    public AudioSource sfx;

    //public GameObject impactEffect;
    public LineRenderer lineRenderer;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        //bulletScript = GetComponent<>();
    }

    public float setBulletSpeed(float speed)
    {
        return speed;
    }

    //[SerializeField] protected float fireRate; //for some reason can't create abstract variable?

    /*  projectile is the bullet prefab
     *  bullet is the rigidbody2d
     *  is using bulletscript's speed
     */
    public abstract void shoot();

    //public abstract IEnumerator shotDelay(float delay);
}
