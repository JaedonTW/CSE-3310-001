using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class Puzzle_Door : Door
{
    public MainCharacter mainCharacter;

    Vector2 xBound;

    /*
        Defining a reference to all possible cell positions to be teleported to.
    */
    private List<Vector3> teleport_Location;

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

    void Fill_Teleport_Locations() 
    {
    
    }

    void Teleport_Character() 
    {
        

    
    }

    private void OnMouseDown()
    {
        Change_Door_State();
        Teleport_Character();

        /*
            The gameObject's tag is the name of
            the door defined in the inspector.
        */
        manager.Track_Door_Order(door_Order, gameObject.tag);
    }



    void Start()
    {
        // Find reference to the GameManager object.
        manager = FindObjectOfType<GameManager>();

        // The door will begin closed.
        animator.SetBool("isOpen", false);

        xBound = new Vector2(-14f, 30f);

        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    void Update()
    {
        // Disregard, just testing something.
        mainCharacter.transform.position = new Vector2(Random.Range(xBound.x, xBound.y), -5);
       
    }
}
