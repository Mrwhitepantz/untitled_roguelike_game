using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public GameObject select;
    public int currentWeapon;
    public Inventory inventory;
    private GameObject activeItem;
    public ItemManager itemManager;
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject garand;
    public GameObject machinegun;

    void Start()
    {
        currentWeapon = 1;
        CurrentSlot(1);
    }

    void UseSelected (int slot)
    {
        int curSlot = slot - 1;
        activeItem= inventory.weaponList[curSlot];
        if (inventory.weaponList[curSlot] != null)
        {
            if (activeItem.name == "ZachMachineGun")
            {
                if (itemManager.playerScript.hasWeapon)
                {
                    itemManager.playerScript.gun.SetActive(false);
                }
                itemManager.equipWeapon(machinegun, machinegun.GetComponent<MachineGun>());
                itemManager.equippedWeapon = machinegun.name;
                machinegun.SetActive(true);
            }
            else if (activeItem.name == "Shotgun")
            {
                if (itemManager.playerScript.hasWeapon)
                {
                    itemManager.playerScript.gun.SetActive(false);
                }
                itemManager.equipWeapon(shotgun, shotgun.GetComponent<Shotgun>());
                itemManager.equippedWeapon = shotgun.name;
                shotgun.SetActive(true);
            }
            else if (activeItem.name == "ZachPistol")
            {
                if (itemManager.playerScript.hasWeapon)
                {
                    itemManager.playerScript.gun.SetActive(false);
                }
                itemManager.equipWeapon(pistol, pistol.GetComponent<Pistol>());
                itemManager.equippedWeapon = pistol.name;
                pistol.SetActive(true);
            }
            else if (activeItem.name == "M1Garand")
            {
                if (itemManager.playerScript.hasWeapon)
                {
                    itemManager.playerScript.gun.SetActive(false);
                }
                itemManager.equipWeapon(garand, garand.GetComponent<M1Garand>());
                itemManager.equippedWeapon = garand.name;
                garand.SetActive(true);
            }
        }
        else
        {
            return;
        }
    }

    void CurrentSlot(int slot)
    {
        currentWeapon = slot;
        if (currentWeapon == 1) 
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(-180, 0);
            UseSelected(slot);
        }
        else if (currentWeapon == 2)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(-90, 0);
            UseSelected(slot);
        }
        else if (currentWeapon == 3)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            UseSelected(slot);
        }
        else if (currentWeapon == 4)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 0);
            UseSelected(slot);
        }
        else if (currentWeapon == 5)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(180, 0);
            UseSelected(slot);
        }
    }
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CurrentSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CurrentSlot(5);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {

        }

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            if (currentWeapon == 1)
            {
                CurrentSlot(2);
            }
            else if (currentWeapon == 2)
            {
                CurrentSlot(3);
            }
            else if (currentWeapon == 3)
            {
                CurrentSlot(4);
            }
            else if (currentWeapon == 4)
            {
                CurrentSlot(5);
            }
            else if (currentWeapon == 5)
            {
                CurrentSlot(1);
            }
        }
        else if (scroll < 0f)
        {
            if (currentWeapon == 2)
            {
                CurrentSlot(1);
            }
            else if (currentWeapon == 3)
            {
                CurrentSlot(2);
            }
            else if (currentWeapon == 4)
            {
                CurrentSlot(3);
            }
            else if (currentWeapon == 5)
            {
                CurrentSlot(4);
            }
            else if (currentWeapon == 1)
            {
                CurrentSlot(5);
            }
        }
    }
}
