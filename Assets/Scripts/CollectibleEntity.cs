using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleEntity : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
