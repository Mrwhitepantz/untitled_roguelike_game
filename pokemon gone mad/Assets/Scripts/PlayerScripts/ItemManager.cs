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
    public string equippedWeapon;
    public PlayerHealth health;
    

    //Zach: added some fields for picking up weapons
    [SerializeField] protected ShootingController shootingController;
    [SerializeField] public Player playerScript;
    //Zach: added fields ends here

    void Start()
    {
        topDownController = player.GetComponent<TopDownController>();
        shootingController = player.GetComponent<ShootingController>();
        playerScript = player.GetComponent<Player>();
        Physics.IgnoreLayerCollision(2,8);
        health = playerScript.GetComponent<PlayerHealth>();
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if (equippedWeapon == collision.gameObject.name){
            return;
        }
        
        if (collision.gameObject.name == "Sunglasses"){
            ifCollision = true;
            topDownController.ifCollision = true;
            topDownController.animator.SetBool("SunglassesItem",true);
            topDownController.maxSpeed=16f; //double speed
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name.ToString().Contains("PotionBlue") ){
            ifCollision = true;
            topDownController.ifCollision = true;
            topDownController.animator.SetBool("PotionBlueItem",true);
            topDownController.maxSpeed = 12f;
            Destroy(collision.gameObject);
        }
        
        
        //Zach: Some code I added for picking up weapons
        if (collision.gameObject.name == "Shotgun")
        {
            //If currently have a weapon equipped, replace it
            if (playerScript.hasWeapon)
            {
                playerScript.gun.SetActive(false);
                //Debug.Log("ItemManager: destroyed Shotgun");
            }
            ifCollision = true;
            equipWeapon(collision.gameObject, collision.gameObject.GetComponent<Shotgun>());
            equippedWeapon=collision.gameObject.name;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
        if (collision.gameObject.name == "ZachMachineGun")
        {
            if (playerScript.hasWeapon)
            {
                playerScript.gun.SetActive(false);
                //Debug.Log("ItemManager: destroyed Machinegun");
            }
            ifCollision = true;
            //Debug.Log("ItemManager: destroyed Machinegun2");
            equipWeapon(collision.gameObject, collision.gameObject.GetComponent<MachineGun>());
            equippedWeapon=collision.gameObject.name;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (collision.gameObject.name == "ZachPistol")
        {
            if (playerScript.hasWeapon)
            {
               playerScript.gun.SetActive(false);
                //Debug.Log("ItemManager: destroyed Pistol");
            }
            ifCollision = true;
            //Zach: equips weapon to the player
            equipWeapon(collision.gameObject, collision.gameObject.GetComponent<Pistol>());
            equippedWeapon=collision.gameObject.name;
            //Zach: turns off box collider, or else gun can be destroyed by enemy bullet
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (collision.gameObject.name == "M1Garand")
        {
            if (playerScript.hasWeapon)
            {
                playerScript.gun.SetActive(false);
                //Debug.Log("ItemManager: destroyed Pistol");
            }
            ifCollision = true;
            //Zach: equips weapon to the player
            equipWeapon(collision.gameObject, collision.gameObject.GetComponent<M1Garand>());
            equippedWeapon = collision.gameObject.name;
            //Zach: turns off box collider, or else gun can be destroyed by enemy bullet
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        //Zach: My code ends here
        StartCoroutine(waiter());
        //Debug.Log("Item Manager: " + collision.gameObject.name);
    }
    public IEnumerator waiter(){
        yield return new WaitForSecondsRealtime(3);

        topDownController.maxSpeed = 8f;
        topDownController.animator.SetBool("SunglassesItem",false);
        topDownController.animator.SetBool("PotionBlueItem",false);
    }

    //Zach:More code
    // Helper function to equip a weapon to the player gameobject
    public void equipWeapon(GameObject gun, Gun gunScript)
    {
        gun.transform.parent=gameObject.transform;
        playerScript.gun = gun;
        playerScript.gunBody = gun.GetComponent<Rigidbody2D>();
        playerScript.hasWeapon = true;
        shootingController.gun = gunScript;
        //Zach: Code snippet citation - https://support.unity.com/hc/en-us/articles/206116386-How-do-I-play-multiple-Audio-Sources-from-one-GameObject-
        shootingController.gun.sfx.PlayOneShot(gunScript.equipSFX, .5f);
        shootingController.hasWeapon = true;
    }
    //Zach:My code ends here
}

