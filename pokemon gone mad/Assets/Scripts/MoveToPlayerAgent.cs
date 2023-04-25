using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class MoveToPlayerAgent : Agent
{
    //Vector3 lastLoc = transform;
    int ran;
    [SerializeField] public GameObject bulletPrefab;
    public Vector3 player;
    public GameObject PC;
    public float lastpos;
    private bool found;
    [SerializeField] private Transform target;
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    private float visualTime = 0.0f;
    
    
    
    private void start(){
        //lastpos = transform.localPosition - target.localPosition;
    }
    
    
    public override void OnEpisodeBegin(){
        //Debug.Log(gameObject.name);
        //Debug.Log(gameObject.transform.position.x);
        ran = Random.Range(0, 4);
        
            if (ran == 0){
                transform.localPosition = new Vector3(4.964f,-3.13f,0);

            }
            
            
            if (ran == 1){
                transform.localPosition = new Vector3(-4.03f,5.4f,0);

            }
            
            //brake;
           if (ran == 2){
            transform.localPosition = new Vector3(-10.75f,-3.09f,0);

            }
            
            //brake;
            if (ran == 3){
                transform.localPosition = new Vector3(-3.95f,-9.6f,0);

            }
            
            //brake;
            if (ran == 4){
                transform.localPosition = new Vector3(8.59f,8.59f,0);

            }
            
            //brake;
            if (ran == 5){
                transform.localPosition = new Vector3(18.73f,-0.84f,0);

            }
            
            //brake;
            if (ran == 6){
                transform.localPosition = new Vector3(2.97f,5.83f,0);

            }
            
            //brake;
            if (ran == 7){
                transform.localPosition = new Vector3(0.11f,-2.84f,0);

            }
            
            //brake;
            if (ran == 8){
                transform.localPosition = new Vector3(23.92f,3.54f,0);

            }
            
            //brake;
            if (ran == 9){
                transform.localPosition = new Vector3(27.82f,-0.27f,0);
            }
            
            //brake;
            
        
        //transform.localPosition=new Vector3(1.564201f,3.037295f,0f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.ContinuousActions[0]);
        float moveX = actions.ContinuousActions[0];
        float moveY= actions.ContinuousActions[1];
        float moveSpeed = 6;
        transform.localPosition+= new Vector3(moveX,moveY,0)*Time.deltaTime* moveSpeed;
        //Debug.Log("actions.DiscreteActions");
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            //Debug.Log("hit");
            SetReward(100f);
            EndEpisode();
        }
        else {
            SetReward(-1f);
            EndEpisode();
        }
        if (collision.tag =="PlayerBullet"){
            SetReward(-10f);
            EndEpisode();
        }
        if(collision.tag == "Wall"){
            SetReward(-1f);
            EndEpisode();

        }

        
    }
    private void FixedUpdate(){
        //SetReward(.05f);
        /*lastpos = Vector3.Distance (PC.position,gameObject.position);
        if (Vector3.Distance (PC.position,gameObject.position) < lastpos){
            SetReward(10f);
        }
        else{
            SetReward(-2f);
        }*/
        
        /*if (transform.localPosition.x -player.transform.localPosition.x <lastLoc.x-player.transform.localPosition.x){}
        lastLoc = new Vector3(
            
            transform.localPosition.y -player.transform.localPosition.y
            transform.localPosition.z -player.transform.localPosition.z
        )
        lastLoc = transform;*/

       
        timer += Time.deltaTime;

        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
        if (timer > waitTime)
        {
            visualTime = timer;

            // Remove the recorded 2 seconds.
            timer = timer - waitTime;
            StartCoroutine(waiter());
        }
        
        

        //bool LineOS = LOS();
        //Debug.Log(LineOS);

        /*if(LineOS ){
            SetReward(1f);

        }
        if(LineOS == false && found){
            StartCoroutine(waiter());
        }*/

    }

    bool LOS(){
        //bood ret = false;
        player = ((GameObject.Find("Typical_Player").transform.position)-transform.position).normalized;
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
        //bool LineOS = LOS();
        
        GameObject projectile = Instantiate(bulletPrefab, PC.transform.position, Quaternion.identity);
        Rigidbody2D bullet = projectile.GetComponent<Rigidbody2D>();
        //bullet.AddForce(gameObject.transform.position*1f, ForceMode2D.Impulse);
        //may need to call OnTrigger2D when implementing damage
        Destroy(projectile, 1.5f); // this will destroy the cloned bullet if it doesn't collide with anything

        yield return new WaitForSecondsRealtime(1.6f);
        
            SetReward(5f);
        
        
        
    }
    

}