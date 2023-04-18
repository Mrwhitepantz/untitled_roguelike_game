using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consider making this an abstract class
public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Transform firePoint; //where the bullets will appear
    [SerializeField] protected GameObject bulletPrefab; //the bullet asset it will shoot, includes sprite, rigidbody2d, and the collider
    [SerializeField] protected Rigidbody2D weapon;
    [SerializeField] protected AudioClip gunshotSFX;
    [SerializeField] public AudioClip equipSFX;
    [SerializeField] public AudioSource sfx;
    [SerializeField] protected LineRenderer lineRenderer; // for pistolRaycast
    //[SerializeField] protected GameObject impactEffect;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    /*public float setBulletSpeed(float speed)
    {
        return speed;
    }*/

    public abstract void shoot();
}
