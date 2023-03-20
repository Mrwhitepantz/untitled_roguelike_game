using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdTempBar : MonoBehaviour
{
    public Slider botSlider;
    public Image botFill;
    public GameObject ice;

    void Start()
    {
        ice.SetActive(false);
    }

    public void SetMinTemp(int temp)
    {
        botSlider.maxValue = (-temp);
        botSlider.value = (-temp);
    }

    public void SetTemp(int temp)
    {
        botSlider.value = (-temp);
        if (botSlider.value == botSlider.maxValue)
        {
            ice.SetActive(true);
        }
        else
        {
            ice.SetActive(false);
        }
    }
}
