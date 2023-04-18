using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToPlayerAgent : Agent
{
    public Vector3 player;
    public GameObject PC;
    public float lastpos;
    private bool found;
    [SerializeField] private Transform target;
    private void start(){
        //lastpos = transform.localPosition - target.localPosition;
    }
    
    
    public override void OnEpisodeBegin(){
        transform.localPosition=new Vector3(1.564201f,3.037295f,0f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.ContinuousActions[0]);
        float moveX = actions.ContinuousActions[0];
        float moveY= actions.ContinuousActions[1];
        float moveSpeed = 1;
        transform.localPosition+= new Vector3(moveX,moveY,0)*Time.deltaTime* moveSpeed;
        //Debug.Log("actions.DiscreteActions");
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            //Debug.Log("hit");
            SetReward(1000f);
            EndEpisode();
        }
        else {
            SetReward(-100f);
            EndEpisode();
        }
        if (collision.tag =="PlayerBullet"){
            SetReward(-20f);
            EndEpisode();
        }
        if(collision.tag == gameObject.tag){
            SetReward(-5f);

        }

        
    }
    private void FixedUpdate(){
        SetReward(1f);
        /*lastpos = Vector3.Distance (PC.position,gameObject.position);
        if (Vector3.Distance (PC.position,gameObject.position) < lastpos){
            SetReward(10f);
        }
        else{
            SetReward(-2f);
        }*/

        bool LineOS = LOS();

        if(LineOS ){
            SetReward(50f);

        }
        if(LineOS == false && found){
            StartCoroutine(waiter());
        }

    }

    bool LOS(){
        //bood ret = false;
        player = ((GameObject.Find("Player").transform.position)-transform.position).normalized;
        //transform.Rotate(Vector3.forward * lookspeed * Time.deltaTime);
        RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, (target.position-transform.position), 1 << LayerMask.NameToLayer("map/objects"));

            if (lineOfSight.collider.tag == "Player"){
                //Debug.Log("I HAVE HIT THE PLAYER !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! FromAI ATTACk");
                Debug.DrawRay(transform.position, (target.position-transform.position), Color.green);
                return true;
            }
            else
            {
                //Debug.Log("I HAVE MISSED THE PLAYER FromAI ATTACk I hit" + lineOfSight.collider.tag);
                Debug.DrawRay(transform.position, (target.position-transform.position), Color.red);
                return false;
            }

        //return false;

    }
    IEnumerator waiter()
    {
        bool LineOS = LOS();
        yield return new WaitForSecondsRealtime(2);
        if(LineOS== false ){
            SetReward(-15f);
        }
        
        
        
    }
    

}