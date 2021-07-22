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
        MainUI.SetActive(false);
       // LevelSelectUI.SetActive(true);
    }
    public void Act1()
    {
        SceneManager.LoadScene("0");
    }
    public void Act2()
    {
        SceneManager.LoadScene("2");
    }
    public void Act3()
    {
        SceneManager.LoadScene("");
    }
}

