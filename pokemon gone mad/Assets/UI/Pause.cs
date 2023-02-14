using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{ 

    public static bool gameIsPaused;
    public GameObject invMenu;
    public GameObject statMenu;

    void Start()
    {
        gameIsPaused = false;
        invMenu.SetActive(false);
        statMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameIsPaused = !gameIsPaused;
            InvPause();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            gameIsPaused = !gameIsPaused;
            StatPause();
        }
    }

    void InvPause()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            invMenu.SetActive(true);
            statMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            invMenu.SetActive(false);
            statMenu.SetActive(false);
        }
    }
    void StatPause()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            statMenu.SetActive(true);
            invMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            statMenu.SetActive(false);
            invMenu.SetActive(false);
        }
    }
}
