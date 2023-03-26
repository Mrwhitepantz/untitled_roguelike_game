using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITesting : MonoBehaviour
{
    public PlayerHealth healthValueTest;
    public HealthBar healthVisualTest;
    public PlayerTemp tempTest;
    public HotTempBar hotTest;
    public ColdTempBar coldTest;
    public Pause pauseTest;
    public Inventory inventoryTest;
    public Hotbar hotbarTest;
    
    int newHealthValue = 100;
    int newSliderValue = 100;
    int newTempValue = 0;
    bool pauseState = false;
    int newHotBarSlot = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthValueTest.DamageToPlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            healthValueTest.HealthToPlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            tempTest.TempToPlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            tempTest.TempToPlayer(-10);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            InventoryPickupTest();
        }
        TestHealthValue();
        TestHealthSliderValue();
        TestTempValue();
        PauseTest();
        HotbarScrollTest();
        OutsideHealthTest();
    }

    // TEST #1 Tests Health Value
    void TestHealthValue()
    {
        if (newHealthValue != healthValueTest.currentHealth) 
        {
            Debug.Log("Current Health Changed To "+healthValueTest.currentHealth);
            newHealthValue = healthValueTest.currentHealth;
            TestMaxMinHealth();
            TestColorChange();
        }
    }

    // TEST #2 Test if Player is at Max Health or Dead
    void TestMaxMinHealth()
    {
        if (newHealthValue == healthValueTest.maxHealth)
        {
            Debug.Log("Player is at Max Health");
        }
        if (newHealthValue == 0)
        {
            Debug.Log("Player is Dead");
        }
    }

    // TEST #3 Test if Slider for HealthBar is working
    void TestHealthSliderValue()
    {
        if (newSliderValue != healthVisualTest.sliderValue)
        {
            Debug.Log("Slider Value is now " + healthVisualTest.sliderValue);
            newSliderValue = healthVisualTest.sliderValue;
        }
    }

    // TEST #4 Test to see if healthbar color is being changed according to player health
    void TestColorChange()
    {
        if (healthVisualTest.sliderColor == healthVisualTest.gradient.Evaluate(1f))
        {
            Debug.Log("Health bar is GREEN");
        }
        if (healthVisualTest.sliderColor == healthVisualTest.gradient.Evaluate(0.4f))
        {
            Debug.Log("Health bar is YELLOW");
        }
        if (healthVisualTest.sliderColor == healthVisualTest.gradient.Evaluate(0.1f))
        {
            Debug.Log("Health bar is RED");
        }
    }

    // TEST #5 Test playing temperature (Normal, Above normal, below normal)
    void TestTempValue()
    {
        if (newTempValue != tempTest.currentTemp)
        {
            Debug.Log("Temperature has changed to "+ tempTest.currentTemp);
            newTempValue = tempTest.currentTemp;
            if (newTempValue > 0)
            {
                Debug.Log("You are Warmer");
                newTempValue = tempTest.currentTemp;
            }
            else if (newTempValue < 0)
            {
                Debug.Log("You are Colder");
                newTempValue = tempTest.currentTemp;
            }
            else
            {
                Debug.Log("You are Neither Warm or Cold");
                newTempValue = tempTest.currentTemp;
            }
            TestOverheating();
            TestFreezing();
        }
    }

    // TEST #6 Test For max player temp, also tests to make sure flame image is appearing
    void TestOverheating()
    {
        if (newTempValue == tempTest.maxTemp) 
        {
            if (hotTest.flame.active == true)
            {
                Debug.Log("Player is OVERHEATING");
            }
            else
            {
                Debug.Log("Player is OVERHEATING BUT FLAME IS NOT SHOWING UP");
            }
        }
    }

    // TEST #7 Test for min player temp, also tests to make sure ice image is appearing
    void TestFreezing()
    {
        if (newTempValue == tempTest.minTemp)
        {
            if (coldTest.ice.active == true)
            {
                Debug.Log("Player is FREEZING");
            }
            else
            {
                Debug.Log("Player is FREEZING BUT ICE IS NOT SHOWING UP");
            }
        }
    }

    // TEST #8 Tests if game is paused or unpaused
    void PauseTest()
    {
        if (pauseState != pauseTest.gameIsPaused)
        {
            if (Time.timeScale == 0)
            {
                Debug.Log("GAME IS PAUSED");
                pauseState = true;
            }
            if (Time.timeScale == 1)
            {
                Debug.Log("GAME IS UNPAUSED");
                pauseState = false;
            }
            InventoryWindowTest();
        }
    }

    // TEST #9 Test to make sure Inventory window opens up with I, closes I or Escape. The actual window openings can be found in Pause.cs
    void InventoryWindowTest()
    {
        if (pauseTest.invMenu.active == true)
        {
            Debug.Log("INVENTORY WINDOW HAS OPENED");
        }
        if (pauseTest.invMenu.active == false)
        {
            Debug.Log("INVENTORY WINDOW HAS CLOSED");
        }
    }

    // TEST #10 Test to see which inventory slots are filled (reads from inventory handler in unity)
    void InventoryPickupTest()
    {
        for (int i = 0; i < 20; i++)
        {
            if (inventoryTest.itemList[i])
            {
                Debug.Log("INVENTORY SLOT " + i + " IS FILLED");
            }
        }
    }

    // TEST # 11 Test mouse Scrolling with hotbar and alpha numbers 1-5
    void HotbarScrollTest()
    {
        if (newHotBarSlot != hotbarTest.currentWeapon)
        {
            if (hotbarTest.currentWeapon == 1)
            {
                Debug.Log("CURRENT HOTBAR SELECT IS 1");
            }
            else if (hotbarTest.currentWeapon == 2)
            {
                Debug.Log("CURRENT HOTBAR SELECT IS 2");
            }
            else if (hotbarTest.currentWeapon == 3)
            {
                Debug.Log("CURRENT HOTBAR SELECT IS 3");
            }
            else if (hotbarTest.currentWeapon == 4)
            {
                Debug.Log("CURRENT HOTBAR SELECT IS 4");
            }
            else if (hotbarTest.currentWeapon == 5)
            {
                Debug.Log("CURRENT HOTBAR SELECT IS 5");
            }
            else
            {
                Debug.Log("HOTBAR VALUE IS OUT OF RANGE");
            }
            newHotBarSlot = hotbarTest.currentWeapon;
        }
    }

    // TEST #12 Check What happens when health is set above 100 (max value) and below 0 (Min Value)
    void OutsideHealthTest()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthValueTest.HealthToPlayer(200);
            Debug.Log("200 HEALTH to player brings Player health to " + healthValueTest.currentHealth);
            newHealthValue = healthValueTest.currentHealth;
            if (healthValueTest.currentHealth != 100)
            {
                Debug.Log("ERROR IN PLAYER HEALTH");
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            healthValueTest.DamageToPlayer(200);
            Debug.Log("200 DAMAGE to player brings Player health to " + healthValueTest.currentHealth);
            newHealthValue = healthValueTest.currentHealth;
            if (healthValueTest.currentHealth != 0)
            {
                Debug.Log("ERROR IN PLAYER HEALTH");
            }
        }
    }
}
