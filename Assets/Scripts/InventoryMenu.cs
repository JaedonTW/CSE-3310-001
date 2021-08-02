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
    public GameManager manager;
    /// <summary>
    /// The text boxes for each weapon in order.
    /// </summary>
    public Text[] textBoxes;
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
        for (int i = 0; i < manager.player.HasWeapon.Length; i++)
            textBoxes[i].color = manager.player.HasWeapon[i] ? Color.red : Color.gray;
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

