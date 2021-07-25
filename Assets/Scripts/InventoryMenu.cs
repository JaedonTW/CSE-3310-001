using System.Collections;
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
    // Update is called once per frame
    void Start()
    {
        pauseMenuUI.SetActive(false);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
     void Resume()
    {
        pauseMenuUI.SetActive(false);
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

    public void Weapon1()
    {
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Weapon2()
    {
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Weapon3()
    {
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

