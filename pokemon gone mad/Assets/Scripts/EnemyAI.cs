using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Vector3 Target;
    public Transform player;
    public float speed;
    public float nextPointDictance;
    public float sooterRange;

    Path path;
    int currentPoint;
    bool atEnd = false;

    Seeker seeker;
    Rigidbody2D rb;
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
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        seeker.StartPath(rb.position, player.position, OnPathComplete);
        InvokeRepeating("UpdatePath", 0f, .5f);

    }
    void UpdatePath() {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, player.position, OnPathComplete);

        }
    }
    void OnPathComplete(Path p) {

        if (!p.error) {

            path = p;
            currentPoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentPoint >= path.vectorPath.Count)
        {
            atEnd = true;
            return;
        }
        else {
            atEnd = false;

        }

        Vector2 dirction = ((Vector2)path.vectorPath[currentPoint] - rb.position).normalized;
        Vector2 force = dirction * speed * Time.deltaTime;
        float range = Vector3.Distance(player.position, transform.position);
        bool LineOS = LOS();
        if (range < sooterRange && LineOS)
        {
            rb.velocity = Vector3.zero;

        }
        else {
            rb.AddForce(force);

        }
        

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentPoint]);
        
        if (distance < nextPointDictance) {
            currentPoint++;
        }
    }
}
