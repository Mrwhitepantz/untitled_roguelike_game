using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float speed = 20f;

    //Doesn't work
    /*public void Update()
    {
        Destroy(gameObject, 2);
    }*/

    void onTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name); //will print out the name of an object it hit
        Destroy(gameObject); // will destroy this bullet
    }
}
