using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacter mainCharacter = collision.GetComponentInParent<MainCharacter>();
        if (mainCharacter != null && mainCharacter.Health < 100)
        {
            mainCharacter.Health = Mathf.Min(100, mainCharacter.Health + healthValue);
            Destroy(gameObject);
        }
    }
}
