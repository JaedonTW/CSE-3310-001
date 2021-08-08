using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public GameObject MainUI;
   public GameObject LevelSelectUI;
    public Button Act1Button;
    public Button Act2Button;
    public Button Act3Button;

    public void Start()
    {
        MainUI.SetActive(true);
        LevelSelectUI.SetActive(false);
        Time.timeScale = 1f;
        //Act2Button.interactable = false;
        // Act3Button.interactable = false;
        var actButtons = new Button[]
        {
            Act1Button,
            Act2Button,
            Act3Button,
        };
        int maxUnlocked = PlayerPrefs.GetInt("Max Unlocked",1);
        if(maxUnlocked < 1)
        {
            Debug.LogWarning("The 'Max Unlocked' value is set to " + maxUnlocked + 
                ", but it should be greater than zero.");
            maxUnlocked = 1;
        }
        for(int i = maxUnlocked; i < actButtons.Length; i++)
        {
            actButtons[i].GetComponentInChildren<Text>().color = Color.gray;
            actButtons[i].interactable = false;
        }
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

