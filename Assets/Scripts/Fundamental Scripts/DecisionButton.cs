using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionButton : MonoBehaviour
{
    private BossFightHandler _bossFightHandler;
    private void OnMouseDown()
    {
        if(gameObject.name.CompareTo("_yesButton") == 0) 
        {
            _bossFightHandler.JoinCthulhu();
            return;
        }

        _bossFightHandler.RejectCthulhu();
    }

    private void Start()
    {
        _bossFightHandler = FindObjectOfType<BossFightHandler>();
    }
}
