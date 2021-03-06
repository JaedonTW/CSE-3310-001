using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    bool IsUsable { get; set; } = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsUsable)
        {
            MainCharacter mainCharacter = collision.GetComponent<MainCharacter>();
            if (mainCharacter != null && mainCharacter.Health < 100)
            {
                mainCharacter.ChangeHealth(100);
                var spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.black;
                IsUsable = false;
            }
        }
    }
}
