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
bool LOS(){
        //bood ret = false;
        Target = ((GameObject.Find("Player").transform.position)-transform.position).normalized;
        //transform.Rotate(Vector3.forward * lookspeed * Time.deltaTime);
        RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, (player.position-transform.position), 1 << LayerMask.NameToLayer("map/objects"));

            if (lineOfSight.collider.tag == "Player"){
                //Debug.Log("I HAVE HIT THE PLAYER !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! FromAI ATTACk");
                Debug.DrawRay(transform.position, (player.position-transform.position), Color.green);
                return true;
            }
            else
            {
                //Debug.Log("I HAVE MISSED THE PLAYER FromAI ATTACk I hit" + lineOfSight.collider.tag);
                Debug.DrawRay(transform.position, (player.position-transform.position), Color.red);
                return false;
            }

        //return false;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bool LineOS = LOS();
        float range = Vector3.Distance(player.position, transform.position);
        if (Time.time > cooldownTime)
        {
            if (range > meleeR && range < shootR && LineOS)
            {
                Instantiate(shot, transform.position, Quaternion.identity);
                cooldownTime = Time.time + shotcooldown;
            }
            else if (range <= meleeR&& LineOS) {
                PC.GetComponent<Rigidbody2D>().AddForce(force * Target);
            }
        }
            //}
            //else {
                //Debug.DrawLine(transform.position, lineOfSight.point, Color.blue);
            //}
        
        
    }
}
