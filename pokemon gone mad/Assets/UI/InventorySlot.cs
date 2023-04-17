using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    public GameObject item;

    public GameObject itemImage;

    public void UpdateSlot()
    {
        if (item != null) // make sure item and itemSprite are not null
        {
            SpriteRenderer itemSprite = item.GetComponent<SpriteRenderer>();
            itemImage.GetComponent<Image>().sprite = itemSprite.sprite;
            itemImage.SetActive(true); // set the itemImage object to active
        }
        else
        {
            itemImage.SetActive(false); // set the itemImage object to inactive
        }
    }
}
