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

    //Zach: added some fields for picking up weapons
    [SerializeField] protected ShootingController shootingController;
    [SerializeField] protected Player playerScript;
    //Zach: added fields ends here

    void Start()
    {
        topDownController = player.GetComponent<TopDownController>();
        shootingController = player.GetComponent<ShootingController>();
        playerScript = player.GetComponent<Player>();
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
        if (collision.gameObject.name == "Shotgun")
        {
            if (playerScript.gun != collision.gameObject)
            {
                Destroy(playerScript.gun);
            }
            ifCollision = true;
            equipWeapon(collision.gameObject, collision.gameObject.GetComponent<Shotgun>());
        }
        if (collision.gameObject.name == "ZachMachineGun")
        {
            if (playerScript.gun != collision.gameObject)
            {
                Destroy(playerScript.gun);
            }
            ifCollision = true;
            equipWeapon(collision.gameObject, collision.gameObject.GetComponent<MachineGun>());
        }
        if (collision.gameObject.name == "ZachPistol")
        {
            if (playerScript.gun != collision.gameObject)
            {
                Destroy(playerScript.gun);
            }
            ifCollision = true;
            equipWeapon(collision.gameObject, collision.gameObject.GetComponent<Pistol>());
        }
        //Zach: My code ends here
        StartCoroutine(waiter());
        Debug.Log("Item Manager: " + collision.gameObject.name);
    }
    public IEnumerator waiter(){
        yield return new WaitForSecondsRealtime(3);

        topDownController.maxSpeed = 8f;
        topDownController.animator.SetBool("SunglassesItem",false);
        topDownController.animator.SetBool("PotionBlueItem",false);
    }

    //Zach:More code
    private void equipWeapon(GameObject gun, Gun gunScript)
    {
        playerScript.gun = gun;
        playerScript.gunBody = gun.GetComponent<Rigidbody2D>();
        playerScript.hasWeapon = true;
        shootingController.gun = gunScript;
        shootingController.gun.sfx.PlayOneShot(equipSFX, 1f);
        shootingController.hasWeapon = true;
    }
    //Zach:My code ends here
}

