using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void setHP(float hp)
    {
        health.transform.localScale = new Vector3(hp, 1f);
    }
}
