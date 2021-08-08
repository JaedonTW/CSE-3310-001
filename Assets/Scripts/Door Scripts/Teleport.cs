using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private MainCharacter mainCharacter;
    private Puzzle_Door[] puzzle_Doors;
    private GameManager manager;
    private Vector2 _startPos;
    
    /// <summary>
    /// Teleport_Character will determine the location to place
    /// the main character after he has entered a door. The main
    /// character and the door will have the same x-axis value; the
    /// main character will have a y-axis value one less than the door
    /// so that the location of the character will be just in front of the door.
    /// </summary>
    /// <returns></returns>
    public Vector2 Teleport_Character()
    {
        Debug.Log(manager.GetCorrectDoorsEntered());
        if(manager.GetCorrectDoorsEntered() < 4) 
        {
            Puzzle_Door puzzle_Door = puzzle_Doors[Random.Range(0, 5)];
            return new Vector2(puzzle_Door.transform.position.x, puzzle_Door.transform.position.y - 1);
        }

        else 
        {
            return _startPos;
        }
    }

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        mainCharacter = FindObjectOfType<MainCharacter>();
        puzzle_Doors = FindObjectsOfType<Puzzle_Door>();

        // Initialize startPos Vector2
        _startPos = new Vector2(-28, 17.81f);
    }


}
