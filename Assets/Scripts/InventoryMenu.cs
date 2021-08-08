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
    private GameManager Manager { get; set; }
    public Button weapon1;
    public Button weapon2;
    public Button weapon3;
    /// <summary>
    /// The text boxes for each weapon in order.
    /// </summary>
    public Text[] textBoxes;
    void Start()
    {
        Manager = FindObjectOfType<GameManager>();
        pauseMenuUI.SetActive(false);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        /*weapon1.interactable = false;
        weapon2.interactable = false;
        weapon3.interactable = false;  // set weapons to disabled*/
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


        for (int i = 0; i < Manager.Player.HasWeapon.Length; i++)
            textBoxes[i].color = Manager.Player.HasWeapon[i] ? Color.red : Color.gray;

        Time.timeScale = 0f;



        for (int i = 0; i < Manager.Player.HasWeapon.Length; i++)
            textBoxes[i].color = Manager.Player.HasWeapon[i] ? Color.red : Color.gray;
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
        print("Choosing weapon");
        Manager.Player.SetActiveWeapon(id);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

