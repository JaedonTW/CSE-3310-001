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
        {"Lightning",1},
        {"Water Ripple",2},
        {"Fire",3},
        {"Lion", 4}
    };

    

    /*
        When we select a teleporting door, we will
        execute the following subroutines.
    */
    private void OnMouseDown()
    {
        // Negate the value of the door state; opening the door
        Change_Door_State();

        // Fade the camera out
        mainCamera.StartCoroutine(mainCamera.Fade_Black(isOpen));

        // Teleport the main character to a random door
        mainCharacter.transform.position = teleport.Teleport_Character();

        // Fade the camera in
        mainCamera.StartCoroutine(mainCamera.Fade_Black(!isOpen));

        // Negate the value of the door state; closing the door
        Change_Door_State();

        // Check if the character pressed the correct door in the correct order
        manager.Track_Door_Order(door_Order, gameObject.tag);
    }

    

    void Start()
    {
        // Find reference to the Teleport object.
        teleport = FindObjectOfType<Teleport>();
        
        // The door will begin closed.
        animator.SetBool("isOpen", false);

        // Find reference to the MainCamera object.
        mainCamera = FindObjectOfType<MainCamera>();

        // Find reference to the GameManager object.
        manager = FindObjectOfType<GameManager>();

        // Find reference to the MainCharacter object.
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    void Update()
    {
    }
}
