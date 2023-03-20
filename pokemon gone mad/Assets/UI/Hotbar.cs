using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public GameObject select;
    public int currentWeapon;
    public List<Item> itemList = new List<Item>();

    void Start()
    {
        currentWeapon = 1;
        CurrentSlot(1);
    }

    void CurrentSlot(int slot)
    {
        currentWeapon = slot;
        if (currentWeapon == 1) 
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(-179, 0);
        }
        else if (currentWeapon == 2)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(-90, 0);
        }
        else if (currentWeapon == 3)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else if (currentWeapon == 4)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 0);
        }
        else if (currentWeapon == 5)
        {
            select.GetComponent<RectTransform>().anchoredPosition = new Vector2(179, 0);
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
        }
    }
}
