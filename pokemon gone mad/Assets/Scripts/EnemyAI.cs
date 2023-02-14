using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
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
        if (range < sooterRange)
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
