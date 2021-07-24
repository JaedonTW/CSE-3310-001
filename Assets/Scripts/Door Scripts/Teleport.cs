﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private MainCharacter mainCharacter;
    private Puzzle_Door[] puzzle_Doors;

    /*
        Teleport_Character will determine the location to place
        the main character after he has entered a door. The main
        character and the door will have the same x-axis value; the
        main character will have a y-axis value one less than the door
        so that the location of the character will be just in front of the door.
    */
    public Vector2 Teleport_Character()
    {
        Puzzle_Door puzzle_Door = puzzle_Doors[Random.Range(0, 6)];
        return new Vector2 (puzzle_Door.transform.position.x, puzzle_Door.transform.position.y -1);
    }

    private void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
        puzzle_Doors = FindObjectsOfType<Puzzle_Door>();
    }


}
