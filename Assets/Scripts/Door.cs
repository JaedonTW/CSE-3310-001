﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textMeshPro;

    /*
        The following boolean will be
        used to maintain the state of the door,
        either opened or closed, which will trigger
        the proper animation to be played.
    */
    private bool isOpen;

    /*
        Change_Door_State() will be used to set the
        correct boolean variables regarding the state
        of the door.
    */
    void Change_Door_State() 
    {
        if (isOpen == false) 
        {
            isOpen = true;
        }

        else 
        {
            isOpen = false;
        }
    }

    private void OnMouseDown()
    {
        Change_Door_State();

        if(isOpen == true) 
        {
            textMeshPro.GetComponent<TextMeshProUGUI>().text = gameObject.tag + " is opened!";
        }

        else
        {
            textMeshPro.GetComponent<TextMeshProUGUI>().text = gameObject.tag + " is closed!";
        }

    }

    
    void Start()
    {
        /*
            The door will start off
            closed.
        */
        isOpen = false;

    }

    
    void Update()
    {
        animator.SetBool("isOpen",isOpen);
    }
}
