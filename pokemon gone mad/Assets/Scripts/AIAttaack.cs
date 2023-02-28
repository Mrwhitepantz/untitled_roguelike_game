using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttaack : MonoBehaviour
{
    public GameObject PC;
    public Vector3 Target;
    public Transform player;
    public GameObject shot;
    public float shotcooldown;
    private float cooldownTime;
    public float lookspeed;
    public float distance;
    public float meleeR;
    public float shootR;
    public float force;
    //private bool canshoot = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Target = ((GameObject.Find("Player").transform.position)-transform.position).normalized;
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
        float range = Vector3.Distance(player.position, transform.position);
        if (Time.time > cooldownTime)
        {
            if (range > meleeR && range < shootR)
            {
                Instantiate(shot, transform.position, Quaternion.identity);
                cooldownTime = Time.time + shotcooldown;
            }
            else if (range <= meleeR) {
                PC.GetComponent<Rigidbody2D>().AddForce(force * Target);
            }
        }
            //}
            //else {
                //Debug.DrawLine(transform.position, lineOfSight.point, Color.blue);
            //}
        
        
    }
}
