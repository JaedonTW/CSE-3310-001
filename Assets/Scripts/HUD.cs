using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Health health;

    // Update is called once per frame
    public void setData(MainCharacter mainCharacter)
    {
        health.setHP((float)mainCharacter.health);    
    }
}
