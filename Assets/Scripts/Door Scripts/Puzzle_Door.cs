using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class Puzzle_Door : Door
{
    private Teleport teleport;

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
        //mainCamera.Fade_To_Black();
        teleport.Teleport_Character();
        
        /*
            The gameObject's tag is the name of
            the door defined in the inspector.
        */
        manager.Track_Door_Order(door_Order, gameObject.tag);
    }

    void Start()
    {
        // Find reference to the Teleport object.
        teleport = FindObjectOfType<Teleport>();
        
        // The door will begin closed.
        animator.SetBool("isOpen", false);   
    }

    void Update()
    {
        /*
            This is in update just to test for a bit.
        */
        mainCamera.Fade_To_Black();
    }
}
