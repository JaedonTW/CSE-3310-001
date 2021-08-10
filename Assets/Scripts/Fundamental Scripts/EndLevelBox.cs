using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelBox : MonoBehaviour
{
    public string nextLevel;
    private MainCamera _mainCamera;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        MainCharacter character = collision.GetComponent<MainCharacter>();
        if(character != null)
        {
            if(nextLevel != "Main")
                _mainCamera.StartCoroutine(_mainCamera.Fade_Black(true));
            character.OnLevelEnd();
            SceneManager.LoadScene(nextLevel);
            Spawner.Spawners.Clear();
            if(PlayerPrefs.GetInt("Max Unlocked", 1) < SceneManager.GetActiveScene().buildIndex + 1)
                PlayerPrefs.SetInt("Max Unlocked", SceneManager.GetActiveScene().buildIndex + 1);
            // counting remaining turned friendlies
            var count = Spawner.UndeadCount == -1? 0 : Spawner.UndeadCount;
            count += FindObjectsOfType<Friendly>().Length;
            foreach (var baddie in FindObjectsOfType<Cultist>())
                if (baddie.name.Contains("TurnedFriendly"))
                    count++;
            PlayerPrefs.SetInt("friendly_counter", count);
        }
    }
    private void Start()
    {
        // Find reference to MainCamera object in the scene.
        _mainCamera = FindObjectOfType<MainCamera>();
    }
}
