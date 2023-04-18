using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdTempBar : MonoBehaviour
{
    public Slider botSlider;
    public Image botFill;
    public GameObject ice;
    public HeatManager heatManager;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        heatManager = player.GetComponent<HeatManager>();
        ice.SetActive(false);
    }

    public void SetMinTemp(float temp)
    {
        botSlider.maxValue = (-temp);
        botSlider.value = (-temp);
    }

    public void SetTemp(float temp)
    {
        botSlider.value = (-temp);
        if (heatManager.frozen)
        {
            ice.SetActive(true);
        }
        else
        {
            ice.SetActive(false);
        }
    }
}
