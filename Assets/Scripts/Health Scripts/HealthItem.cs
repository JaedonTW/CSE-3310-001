using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacter mainCharacter = collision.GetComponentInParent<MainCharacter>();
        if (mainCharacter != null && mainCharacter.health < 100)
        {
            mainCharacter.health = Mathf.Min(100, mainCharacter.health + healthValue);
            Destroy(gameObject);
        }
    }
}
