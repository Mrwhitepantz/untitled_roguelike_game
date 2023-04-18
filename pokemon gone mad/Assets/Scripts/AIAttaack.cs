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
    public EnemyAI EnemyAI;
    int type;
    //private bool canshoot = true;
    // Start is called before the first frame update
    public static float meleeD(GameObject Badguy, int mod){
        //Debug.Log(mod+" damageMod used From AI ATTACk");
        if (Badguy.tag == "Squrtal"){
            return (25.5f *mod);
        }
        else if (Badguy.tag == "TestSquare"){
            return 50f*mod;
        }
        else if (Badguy.tag == "Boss"){
            return 80f*mod;
        }
        else if (Badguy.tag == "Charmander"){
            return 36.2f*mod;
        }
        else if (Badguy.tag == "Pikachu"){
            return 15.3f*mod;
        }
        else if (Badguy.tag == "GameHazard"){
            return 8.9f*mod;
        }
        return 0;
    }
    public static int attackType(float distance, float mR, float sR){
        if (distance > mR && distance < sR )
            {
                return (1);
            }
        else if (distance <= mR) {
                return (2);
            }
        else{
            return (3);
        }

    }
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
        type = attackType(range, meleeR, shootR);
        if (Time.time > cooldownTime && LineOS)
        {
            if (type==1)
            {
                EnemyAI.animator.SetFloat("attack",1);
                Instantiate(shot, transform.position, Quaternion.identity);
                cooldownTime = Time.time + shotcooldown;
            }
            else if (type==2) {
                Debug.Log(meleeD(gameObject,1)+"force used From AI ATTACk");
                PC.GetComponent<Rigidbody2D>().AddForce(meleeD(gameObject, 1 ) * Target);
            }
        }
        
            //}
            //else {
                //Debug.DrawLine(transform.position, lineOfSight.point, Color.blue);
            //}
        
        
    }
}
