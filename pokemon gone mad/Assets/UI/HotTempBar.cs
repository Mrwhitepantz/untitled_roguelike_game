using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotTempBar : MonoBehaviour
{
    public Slider topSlider;
    public Image topFill;
    public GameObject flame;

    void Start()
    {
        flame.SetActive(false);
    }

    public void SetMaxTemp(int temp)
    {
        topSlider.maxValue = temp;
        topSlider.value = temp;
    }

    public void SetTemp(int temp)
    {
        topSlider.value = temp;
        if (topSlider.value == topSlider.maxValue)
        {
            flame.SetActive(true);
        }
        else
        {
            flame.SetActive(false);
        }
    }
}
