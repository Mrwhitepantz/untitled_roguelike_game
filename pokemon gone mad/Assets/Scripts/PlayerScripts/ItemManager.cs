using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ItemManager : MonoBehaviour
// {
//     private void OnTriggerEnter2D (Collider2D){
//         return;
//     }
// }

public class ItemManager : MonoBehaviour{
    public GameObject player;
    private TopDownController topDownController;
    public bool ifCollision = false;

    void Start()
    {
        topDownController = player.GetComponent<TopDownController>();
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "Sunglasses"){
            ifCollision = true;
            topDownController.ifCollision = true;
            topDownController.animator.SetBool("SunglassesItem",true);
            topDownController.maxSpeed=16f; //double speed
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "PotionBlue"){
            ifCollision = true;
            topDownController.ifCollision = true;
            topDownController.animator.SetBool("PotionBlueItem",true);
            topDownController.maxSpeed = 4f;
            Destroy(collision.gameObject);
        }
        
        //Zach: Some code I added for picking up weapons
        if (collision.gameObject.name == "Pistol")
        {
            ifCollision = true;
        }
        if (collision.gameObject.name == "Machinegun")
        {

        }
        //Zach: My code ends here
        StartCoroutine(waiter());
        Debug.Log(collision.gameObject.name);
    }
    public IEnumerator waiter(){
        yield return new WaitForSecondsRealtime(3);

        topDownController.maxSpeed = 8f;
        topDownController.animator.SetBool("SunglassesItem",false);
        topDownController.animator.SetBool("PotionBlueItem",false);
}
}

