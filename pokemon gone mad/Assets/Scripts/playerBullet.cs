using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    //public Rigidbody2D body;

    void Start()
    {
        //Rigidbody2D body = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
    }
}
