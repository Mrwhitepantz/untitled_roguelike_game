using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject[] itemList = new GameObject[20];
    public GameObject[] weaponList = new GameObject[5];
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public List<InventorySlot> hotBarSlots = new List<InventorySlot>();


    private bool Add(GameObject item)
    {
        for(int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
            {
                itemList[i] = item;
                inventorySlots[i].item = item;
                return true;
            }
        }
        return false;
    }

    private bool NewWeapon(GameObject item)
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            if (weaponList[i] == null)
            {
                weaponList[i] = item;
                hotBarSlots[i].item = item;
                return true;
            }
        }
        return false;
    }

    public void UpdateSlotUI()
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].UpdateSlot();
        }
    }

    public void UpdateHotBarUI()
    {
        for (int i = 0; i < hotBarSlots.Count; i++)
        {
            hotBarSlots[i].UpdateSlot();
        }
    }

    public void AddItem(GameObject item)
    {
        bool hasAdded = Add(item);

        if (hasAdded)
        {
            UpdateSlotUI();
        }
    }

    public void AddWeapon(GameObject item)
    {
        bool hasAdded = NewWeapon(item);

        if (hasAdded)
        {
            UpdateHotBarUI();
        }
    }
}
