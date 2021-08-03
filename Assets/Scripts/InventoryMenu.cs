﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Button InventoryButton;
    public GameObject weaponSelectUI;
    public GameManager manager;
    public Button weapon1;
    public Button weapon2;
    public Button weapon3;
    // Update is called once per frame
    void Start()
    {
        pauseMenuUI.SetActive(false);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        weapon1.interactable = false;
        weapon2.interactable = false;
        weapon3.interactable = false;  // set weapons to disabled
    }
     void Resume()
    {
        pauseMenuUI.SetActive(false);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void MainMenuSwitch()
    {
        SceneManager.LoadScene("Main");
    }
     public void WeaponSelect()
    {
        weaponSelectUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
    }
    public void clicked()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void SetWeapon(int id)
    {
        manager.player.SetActiveWeapon(id);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

