using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacter mainCharacter = collision.GetComponentInParent<MainCharacter>();
        if (mainCharacter != null && mainCharacter.Health < 100)
        {
            mainCharacter.Health = 100;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.black;
        }
    }
}
