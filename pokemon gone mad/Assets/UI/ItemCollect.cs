using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Item collided with player");
            if (inventory != null)
            {
                Debug.Log("Item collided with " + collision.gameObject.name);
                if (gameObject.CompareTag("Weapon"))
                {
                    inventory.AddWeapon(gameObject);       
                }
                else if (gameObject.CompareTag("InvItem"))
                {
                    inventory.AddItem(gameObject);

                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Invalid item collected.");
                }
            }
            else
            {
                Debug.LogWarning("Inventory component not found on player");
            }
        }
    }
}