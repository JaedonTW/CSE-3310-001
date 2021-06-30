using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UITMpro : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        /*
            Starting Text
        */
        textMeshPro.GetComponent<TextMeshProUGUI>().text = "CHOOSE YOUR FATE!";
    }

    void Update()
    {
        
    }
}
