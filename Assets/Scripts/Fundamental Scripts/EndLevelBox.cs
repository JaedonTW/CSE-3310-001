using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelBox : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacter character = collision.GetComponent<MainCharacter>();
        if(character != null)
        {
            character.OnLevelEnd();
            SceneManager.LoadScene("Level2 - Library");
            Spawner.Spawners.Clear();
        }
    }
}
