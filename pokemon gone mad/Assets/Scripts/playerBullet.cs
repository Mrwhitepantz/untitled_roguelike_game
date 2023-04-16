using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Rigidbody2D bullet;
    [SerializeField] public float speed = 20f;


    // Going to have to move this to somewhere else because it interfere's with on trigger
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "BadMen")
        {
            Debug.Log("playerBullet: hit an enemy");
            Destroy(this.gameObject);
        } 
        else if (hitInfo.tag == "Player") // trying to prevent bullet from hitting player
        {
            Debug.Log("playerBullet: hit player");
            Destroy(this.gameObject);
        }
        else if (hitInfo.tag != "BadMen")
        {
            Debug.Log("playerBullet: hit an environment");
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.bulletPrefab, 1);
            Debug.Log("playerBullet: destroyed in 1 sec");
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
        // Can add health and other collision related things
    }

    //private void OnCollisionEnter()
}
