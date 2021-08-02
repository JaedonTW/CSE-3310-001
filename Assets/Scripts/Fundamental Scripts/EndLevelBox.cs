using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelBox : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacter character = collision.GetComponent<MainCharacter>();
        if(character != null)
        {
            print("Level end!");
        }
    }
}
