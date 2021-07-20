using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle_Door : Door
{
    /*
        door_Order is a SortedDictionary that we will use to
        determine the order in which Harris must enter the doors
        to finish level 2. 
    */
    SortedDictionary<string, int> door_Order = new SortedDictionary<string, int>()
    {
        {"Viper",0},
        {"Reed",1},
        {"Water Ripple",2},
        {"Vulture",3},
        {"Lion", 4}
    };

    private void OnMouseDown()
    {
        Change_Door_State();

        /*
            The gameObject's tag is the name of
            the door defined in the inspector.
        */
        manager.Track_Door_Order(door_Order, gameObject.tag);

        if (isOpen == true)
        {
            // Sound Maybe ?
            Debug.Log("OPENING!\n");
        }

        else
        {
            Debug.Log("CLOSING\n");
            // Sound Maybe ?
        }
    }

    void Start()
    {
        // Find reference to the GameManager object.
        manager = FindObjectOfType<GameManager>();

        // The door will begin closed.
        isOpen = false;
    }

    void Update()
    {
        animator.SetBool("isOpen", isOpen);
    }
}
