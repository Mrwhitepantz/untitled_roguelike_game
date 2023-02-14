using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : MonoBehaviour
{
    public GameObject shot;
    public float shotcooldown;
    private float cooldownTime;
    public float lookspeed;
    public float distance;
    //private bool canshoot = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * lookspeed * Time.deltaTime);
        RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, transform.right, distance);
        Debug.DrawRay(transform.position, lineOfSight.point, Color.black);
        if (lineOfSight.collider != null)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.green);
        }
        else
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.black);
        }
        //if (lineOfSight.collider.tag == "Player")
        //{
        
                if (Time.time > cooldownTime)
                {
                    Instantiate(shot, transform.position, Quaternion.identity);
                    cooldownTime = Time.time + shotcooldown;
                }
            //}
            //else {
                //Debug.DrawLine(transform.position, lineOfSight.point, Color.blue);
            //}
        
        
    }
}
