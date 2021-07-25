using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public GameObject MainUI;
   public GameObject LevelSelectUI;

    public void Start()
    {
        MainUI.SetActive(true);
        LevelSelectUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void EndGame()
    {
        Debug.Log("Done");
        Application.Quit();
    }

    public void Play()
    {
        Debug.Log("Switch");
        MainUI.SetActive(false);
        LevelSelectUI.SetActive(true);
    }
    public void Act1()
    {
        Debug.Log("Level1");
        SceneManager.LoadScene("Level1 - Mansion");
    }
    public void Act2()
    {
        Debug.Log("Level2");
        SceneManager.LoadScene("Level2 - Library");
    }
    public void Act3()
    {
        Debug.Log("Level3");
        SceneManager.LoadScene("Level3 - Boss");
    }
}

