using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public Item[] itemList = new Item[20];
    public Item[] weaponList = new Item[5];
    public List<InventorySlot> inventorySlots = new List<InventorySlot> ();
    public List<InventorySlot> hotBarSlots = new List<InventorySlot>();

    private bool Add(Item item)
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

    private bool NewWeapon(Item item)
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

    public void AddItem(Item item)
    {
        bool hasAdded = Add(item);

        if (hasAdded)
        {
            UpdateSlotUI();
        }
    }

    public void AddWeapon(Item item)
    {
        bool hasAdded = NewWeapon(item);

        if (hasAdded)
        {
            UpdateHotBarUI();
        }
    }
}
