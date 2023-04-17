using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    //[SerializeField] protected Rigidbody2D bullet;
    //[SerializeField] public float speed = 20f;

    //Zach: Bullet collision for exceptions
    public void Update()
    {
        if (bulletPrefab.name == "shotgunBullet(Clone)")
        {
            Destroy(this.gameObject, .25f);
        } 
        else //Zach: tried to have this in OnTriggerEnter2D, but it never executes
        {
            Destroy(this.gameObject, 1);
        }
        
    }

    // Collision for player bullets
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "BadMen")
        {
            Debug.Log("playerBullet: hit an enemy");
            Destroy(this.gameObject);
        } 
        else if (hitInfo.tag == "Player")
        {
            Debug.Log("playerBullet: hit player");
        }
        else if (hitInfo.tag == "EnvironmentDecorations")
        {
            Debug.Log("playerBullet: hit an environment");
            Destroy(this.gameObject);
        }
    }

    //private void OnCollisionEnter()
}
