using System.Collections;
using UnityEngine;

public class HeatManager : MonoBehaviour
{
    public float temperatureLevel = 0;
    public bool overHeated = false;
    public bool frozen = false;
    [SerializeField]
    private RoomManager room;
    private readonly float maxHeat = 60f;
    private readonly float maxFreeze = -60f;
    private readonly float baseTemp = 0f;
    private bool heating = false;
    private bool freezing = false;
    private bool heatCoroutine = false;
    private bool freezeCoroutine = false;
    private bool returnToBase = false;

    void FixedUpdate()
    {
        Biome biome = room.GetRoomBiome();
        if (biome is DesertBiome)
        {
            Debug.Log("DesertBiome");
            freezing = false;
            heating = true;
            if (!heatCoroutine && !frozen)
            {
                StartCoroutine(HeatingCoroutine());
            }
        }
        else if (biome is SnowyBiome)
        {
            heating = false;
            freezing = true;
            if (!freezeCoroutine && !overHeated)
            {
                StartCoroutine(FreezingCoroutine());
            }
        }
        else
        {
            heating = false;
            freezing = false;
            if (temperatureLevel != 0 && !returnToBase && !overHeated && !frozen)
            {
                StartCoroutine(ReturnToBaseTemperatureLevelCoroutine());
            }
        }
    }

    private IEnumerator ReturnToBaseTemperatureLevelCoroutine()
    {
        // returns the temperature level to 0 at twice the normal speed
        returnToBase = true;
        if (!heating)
        {
            while (temperatureLevel >= 0 && !heating)
            {
                temperatureLevel = Mathf.Clamp(temperatureLevel - 2 * Time.deltaTime, baseTemp, maxHeat);
                yield return null;
            }
        }
        if (!freezing)
        {
            while(temperatureLevel <= 0 && !freezing)
            {
                temperatureLevel = Mathf.Clamp(temperatureLevel + 2 * Time.deltaTime, maxFreeze, baseTemp);
                yield return null;
            }
        }
        returnToBase = false;
    }

    public IEnumerator HeatingCoroutine()
    {
        // increases temperature level by 1 per second until reaching 60 and then starts the overheat coroutine
        heatCoroutine = true;
        while(temperatureLevel < maxHeat && !overHeated)
        {
            temperatureLevel = Mathf.Clamp(temperatureLevel + Time.deltaTime, baseTemp, maxHeat);
            if (!heating)
            {
                heatCoroutine = false;
                StartCoroutine(ReturnToBaseTemperatureLevelCoroutine());
                yield break;
            }
            yield return null;
        }
        if (!overHeated)
        {
            StartCoroutine(OverHeatCo());
        }
    }

    private IEnumerator FreezingCoroutine()
    {
        // decreases temperature level by 1 per second until reaching -60 and then starts the frozen coroutine
        freezeCoroutine = true;
        while (temperatureLevel > maxFreeze && !frozen)
        {
            temperatureLevel = Mathf.Clamp(temperatureLevel - Time.deltaTime, maxFreeze, baseTemp);
            if (!freezing)
            {
                freezeCoroutine = false;
                StartCoroutine(ReturnToBaseTemperatureLevelCoroutine());
                yield break;
            }
            yield return null;
        }
        if (!frozen)
        {
            StartCoroutine(FrozenCo());
        }
    }

    private IEnumerator OverHeatCo()
    {
        // sets overheat tag and while over half-heat decreases the temperature by 1*s.
        // at with 60 max heat, it should take 1 minute to overheat and then be overheated for 30 seconds
        // then start overheating again, only taking 30 seconds this time
        overHeated = true;
        while(temperatureLevel >= maxHeat / 2)
        {
            temperatureLevel -= Time.deltaTime;
            yield return null;
        }
        heatCoroutine = false;
        overHeated = false;
    }

    private IEnumerator FrozenCo()
    {
        // sets frozen tag and while below half-freeze increases the temperature by 15*s.
        // at with -60 max freeze, it should take 1 minute to freeze and then be frozen for 2 seconds
        // then start freezing again, only taking 30 seconds this time
        frozen = true;
        while (temperatureLevel <= maxFreeze / 2)
        {
            temperatureLevel += Time.deltaTime * 15f;
            yield return null;
        }
        freezeCoroutine = false;
        frozen = false;
    }
}
