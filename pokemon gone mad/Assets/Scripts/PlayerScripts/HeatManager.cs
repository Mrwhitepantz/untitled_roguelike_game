using System.Collections;
using UnityEngine;

public class HeatManager : MonoBehaviour
{
    public float temperatureLevel = 0;
    public bool overHeated = false;
    public bool frozen = false;
    [SerializeField]
    private RoomManager room;
    private readonly float maxHeat = 30f;
    private readonly float maxFreeze = -30f;
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
        overHeated = true;
        while(temperatureLevel >= maxHeat / 2)
        {
            temperatureLevel -= Time.deltaTime / 2f;
            yield return null;
        }
        heatCoroutine = false;
        overHeated = false;
    }

    private IEnumerator FrozenCo()
    {
        frozen = true;
        while (temperatureLevel <= maxFreeze / 2)
        {
            temperatureLevel += Time.deltaTime / 2f;
            yield return null;
        }
        freezeCoroutine = false;
        frozen = false;
    }
}
