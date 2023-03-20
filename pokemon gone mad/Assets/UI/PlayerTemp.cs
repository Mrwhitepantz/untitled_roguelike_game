using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTemp : MonoBehaviour
{
    public int minTemp = -60;
    public int maxTemp = 60;
    public int currentTemp;
    public bool freezing = false;
    public bool overheat = false;

    public HotTempBar hotBar;
    public ColdTempBar coldBar;

    void Start()
    {
        currentTemp = 0;
        hotBar.SetMaxTemp(maxTemp);
        coldBar.SetMinTemp(minTemp);
        hotBar.SetTemp(currentTemp);
        coldBar.SetTemp(currentTemp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TempToPlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            TempToPlayer(-10);
        }
    }

    void TempToPlayer(int temp)
    {
        currentTemp += temp;

        if (currentTemp > 0)
        {
            if (currentTemp > maxTemp)
            {
                currentTemp = maxTemp;
                overheat = true;
            }
            else
            {
                hotBar.SetTemp(currentTemp);
                coldBar.SetTemp(0);
                freezing = false; 
                overheat = false;
            }
        }
        else if (currentTemp < 0)
        {
            if (currentTemp < minTemp)
            {
                currentTemp = minTemp;
                freezing = true;
            }
            else
            {
                coldBar.SetTemp(currentTemp);
                hotBar.SetTemp(0);
                freezing = false;
                overheat = false;
            }
        }
        else
        {
            currentTemp = 0;

            hotBar.SetTemp(currentTemp);
            coldBar.SetTemp(currentTemp);
        }
    }
}
