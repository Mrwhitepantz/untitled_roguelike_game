using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float speed = 20f;
    public GameObject impactEffect;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // It works!
        if (hitInfo.tag == "BadMen")
        {
            Debug.Log("hit an enemy");
            Destroy(gameObject);
        } 
        else
        {
            Destroy(gameObject, 2);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        // Can add health and other collision related things
    }
}
