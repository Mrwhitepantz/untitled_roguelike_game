using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Zach: For future reference, I should extend this base class into 2, automatic and single action
 * Automatic weapons don't need to reload, or decrement clip, but single action does need to */
public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Transform firePoint; //where the bullets will appear
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Rigidbody2D weapon;
    [SerializeField] protected AudioClip gunshotSFX;
    [SerializeField] protected AudioClip reloadSFX; //Zach: not all weapons will have this
    [SerializeField] public AudioClip equipSFX;
    [SerializeField] public AudioSource sfx;
    [SerializeField] protected LineRenderer lineRenderer; // for pistolRaycast
    //[SerializeField] protected GameObject impactEffect;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    public abstract void decrementClip();
    public abstract void reload();
    public abstract bool isReloading();
    public abstract int getAmmoCount();
    public abstract void shoot();
}
