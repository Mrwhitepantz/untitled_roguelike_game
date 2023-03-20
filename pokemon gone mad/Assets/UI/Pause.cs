using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{ 

    public static bool gameIsPaused;
    public GameObject invMenu;
    public GameObject statMenu;
    public GameObject pauseMenu;

    void Start()
    {
        gameIsPaused = false;
        invMenu.SetActive(false);
        statMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InvPause();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StatPause();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    void InvPause()
    {
        if ((pauseMenu.active != true) && (statMenu.active != true))
        {
            gameIsPaused = !gameIsPaused;
            if (gameIsPaused)
            {
                Time.timeScale = 0;
                invMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                invMenu.SetActive(false);
            }
        }
    }
    void StatPause()
    {
        if ((pauseMenu.active != true) && (invMenu.active != true))
        {
            gameIsPaused = !gameIsPaused;
            if (gameIsPaused)
            {
                Time.timeScale = 0;
                statMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                statMenu.SetActive(false);
            }
        }
    }
    void PauseMenu()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            Time.timeScale = 0;
            statMenu.SetActive(false);
            invMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            statMenu.SetActive(false);
            invMenu.SetActive(false);
            pauseMenu.SetActive(false);
        }
    }
}
