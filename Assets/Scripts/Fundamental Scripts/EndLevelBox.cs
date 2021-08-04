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
        _mainCamera.StartCoroutine(_mainCamera.Fade_Black(true));

        MainCharacter character = collision.GetComponent<MainCharacter>();
        if(character != null)
        {
            character.OnLevelEnd();
            SceneManager.LoadScene(nextLevel);
            Spawner.Spawners.Clear();
        }
    }
<<<<<<< HEAD
}
=======

    private void Start()
    {
        // Find reference to MainCamera object in the scene.
        _mainCamera = FindObjectOfType<MainCamera>();
    }
}
>>>>>>> ebe5c6a07d735e8acd9b6fe78df5e2642f82e639
