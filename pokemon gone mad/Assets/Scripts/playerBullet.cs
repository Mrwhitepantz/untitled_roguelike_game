using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float speed = 20f;
    //public GameObject impactEffect;


    // Going to have to move this to somewhere else because it interfere's with on trigger
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // It works!
        if (hitInfo.tag == "BadMen")
        {
            Debug.Log("playerBullet: hit an enemy");
            Destroy(this);
        } 
        else if (hitInfo.tag != "BadMen")
        {
            Debug.Log("playerBullet: hit an environment");
            Destroy(this);
        }
        else
        {
            Destroy(this, 1);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);
        // Can add health and other collision related things
    }

    //private void OnCollisionEnter()
}
