using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Vector3 Target;
    private Transform player ;
    public float speed;
    public float nextPointDictance;
    public float sooterRange;
    public GameObject findP;
    private int elte;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float hp = 100;
    public GameObject health;
    public GameObject zoom;

    Path path;
    int currentPoint;
    bool atEnd = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        findP = (GameObject.Find("Player"));
        animator = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        player = findP.transform;
        seeker.StartPath(rb.position, player.position, OnPathComplete);
        InvokeRepeating("UpdatePath", 0f, .5f);
        elte = Random.Range(0,2);
        
        Debug.Log(player);
    
    }
    // Start is called before the first frame update
   public static float MoveSpeed(GameObject enemyType, int type){
        if (enemyType.tag == "Squrtal"){
            if (type == 1){
                return (30);
            }
            else{
                return (20);
            }
            
        }
        else if (enemyType.tag == "TestSquare"){
            if (type == 1){
                return (300);
            }
            else{
                return (200);
            }
        }
        else if (enemyType.tag == "Boss"){
            if (type == 1){
                return (400);
            }
            else{
                return (350);
            }
        }
        else if (enemyType.tag == "Charmander"){
            if (type == 1){
                return (30);
            }
            else{
                return (20);
            }
        }
        else if (enemyType.tag == "Pikachu"){
            if (type == 1){
                return (30);
            }
            else{
                return (20);
            }
        }
        else if (enemyType.tag == "GameHazard"){
            if (type == 1){
                return (30);
            }
            else{
                return (20);
            }
        }
        return 0;
    }
    bool LOS(GameObject TargetLock){
        //bood ret = false;
        Target = ((TargetLock.transform.position)-transform.position).normalized;
        //transform.Rotate(Vector3.forward * lookspeed * Time.deltaTime);
        //player = (GameObject.Find("Player").transform);
        player = findP.transform;
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
    
    void UpdatePath() {
        if (seeker.IsDone()) {
            //player = (GameObject.Find("Player").transform);
            player = findP.transform;
            seeker.StartPath(rb.position, player.position, OnPathComplete);

        }
    }
    void OnPathComplete(Path p) {

        if (!p.error) {

            path = p;
            currentPoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            
            
            hp -= 10;
            if (hp <= 0)
            {
                Vector3 position = transform.position;
                int temp = Random.Range(1,5);
                if (temp == 1){
                    Instantiate(health, transform.position, Quaternion.identity);
                } 
                if (temp == 2){
                    Instantiate(zoom, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            } else {
                animator.SetTrigger("damage");
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //player = (GameObject.Find("Player").transform);
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
        //Debug.Log(dirction);
        animate(dirction);
        Vector2 force = dirction * speed * Time.deltaTime;
        //player = (GameObject.Find("Player").transform);
        player = findP.transform;
        float range = Vector3.Distance(player.position, transform.position);
        //findP = (GameObject.Find("Player"));
        bool LineOS = LOS(findP);
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
    public void animate(Vector2 inputDir)
    {
        

        //Debug.Log(inputDir.x);
        //Debug.Log(inputDir.y);
        
        if (inputDir.x != 0 ){
            animator.SetFloat("speed", 1);
            if (inputDir.x > 0) {
                animator.SetFloat("horizontal", 1);
            } else { animator.SetFloat("horizontal", -1);}
        } else {animator.SetFloat("horizontal", 0);}
        if (inputDir.y != 0 ){
            
            animator.SetFloat("speed", 1);
            if (inputDir.y > 0) {
                animator.SetFloat("vertical", 1);
            } else { animator.SetFloat("vertical", -1);}
            
        } else {animator.SetFloat("vertical", 0);}
        if ((inputDir.x == 0) && (inputDir.y == 0)) {
            
            animator.SetFloat("speed", 0);
        }
    }
}
