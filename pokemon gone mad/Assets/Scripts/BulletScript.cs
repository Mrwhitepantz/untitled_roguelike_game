using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 target;
    public float speed;
    public Vector3 player;
    //public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform.position;
        target = (player - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 dirction = ((Vector2)target - rb.position).normalized;
        //Vector2 force = dirction * speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //rb.AddForce(force);
        float angle = target.y/target.x;
        angle =  Mathf.Rad2Deg*Mathf.Atan(angle);
        if(target.x<0){
            if (target.y>0){
                angle = target.y/target.x;
                angle =  Mathf.Rad2Deg*Mathf.Atan(angle);
            }
        }
        if(target.x>0){

            angle = target.y/target.x;
            angle =  Mathf.Rad2Deg*Mathf.Atan(angle)+180;
            
            
        }
        rb.rotation = angle;
        //Debug.Log(rb.rotation);
        transform.position += target * speed * Time.deltaTime;

        StartCoroutine(waiter());
      
            
                
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet"){
            return;
        }
        if (collision.tag != "Squrtal" ) {
            if (collision.tag !="EnemyBullet"){
                if (collision.tag !="EnvironmentDecorations"){
                Debug.Log("hit");
            //Destroy(gameObject);

            }

            }
            
        }
        
    }
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(2);
        
        Destroy(gameObject);

        
        

        
    }
}
