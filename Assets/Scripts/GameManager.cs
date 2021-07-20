using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public MainCharacter player;
    public Friendly[] friendlies;
    public int CurrentLevel { get; private set; } = 0;
    public bool[,] PathMap { get; private set; }
    public Vector2 MapOffset { get; private set; }
    /*
        correct_Doors_Entered will hold the number of
        doors entered in a row by the user. The user 
        will need to enter 4 correct doors in a row to 
        finish the level.
    */
    private int correct_Doors_Entered;

    /*
        Check_Distance uses the famed Distance Formula
        to check the distance between two entities. 
        If the distance is less than or equal to 5 world units, 
        the Pop-up Text Box will spawn.
    */
    public bool Check_Distance(Transform obj1, Transform obj2)
    {
        float obj1_XPos = obj1.transform.position.x;
        float obj1_YPos = obj1.transform.position.y;
        float obj2_XPos = obj2.transform.position.x;
        float obj2_YPos = obj2.transform.position.y;

        float x_Val = Mathf.Sqrt(Mathf.Pow(obj1_XPos - obj2_XPos, 2));
        float y_Val = Mathf.Sqrt(Mathf.Pow(obj1_YPos - obj2_YPos, 2));
        float instantiation_Distance = 5;

        Debug.Log(Mathf.Sqrt(x_Val + y_Val));

        if (Mathf.Sqrt(x_Val + y_Val) <= instantiation_Distance)
        {
            return true;
        }

        else
        {
            return false;
        }
    }


    public void StartLevel(int level)
    {
        throw new System.NotImplementedException();
    }
    public void ToMainMenu()
    {
        throw new System.NotImplementedException();
    }

    /*
        Track_Door_Order will keep track of if the user entered the correct
        door in the correct order. A Disctionary lookup will be utilized to
        ensure that the Name of the Door and it's value in the door order
        match.
    */
    public void Track_Door_Order(SortedDictionary<string,int> door_Order, string door_Name) 
    {
        /*
            Before we begin traversing the dictionary,
            we have not yet found the KeyValuePair. For
            this reason, found will always start as false.
        */
        
        // We check if the door entered was the correct one. 
        if (door_Order[door_Name] == correct_Doors_Entered)
        {
            // If the door enterd was correct, we increment and
            //   check if all of the correct doors were entered.
            if(++correct_Doors_Entered == 5)
                /*
                    If all correct doors were entered,
                    you may pass to the next stage.
                */
                Debug.Log("YOU WIN !!!\n");
        }
        else
            /*
                If the user enters the wrong door, 
                the correct_Door_Counter starts back
                at 0.
            */
            correct_Doors_Entered = 0;
    }
    void OnMapLoad()
    {
        // initializing the 'PathMap' and the 'MapOffset'
        // getting walls
        var tilemaps = FindObjectsOfType<Tilemap>();
        Tilemap walls = null;
        foreach(var tilemap in tilemaps)
            if(tilemap.name == "Walls")
            {
                walls = tilemap;
                break;
            }
        if (walls == null)
            throw new MissingComponentException("There must be a 'Tilemap' component named 'Walls'.");
        // setting up PathMap
        var bounds = walls.cellBounds;
        var tiles = walls.GetTilesBlock(bounds);
        PathMap = new bool[bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin];
        for (int i = 0; i < PathMap.GetLength(0); i++)
            for (int j = 0; j < PathMap.GetLength(1); j++)
                if (tiles[i + j * PathMap.GetLength(0)] == null)
                    PathMap[i, j] = true;
        // getting offset from the world to the center of Cell[0,0]
        // we do it like this so that each grid position will corrospond to the center of that tile.
        MapOffset = new Vector2(walls.transform.position.x + walls.cellSize.x * 0.5f,
            walls.transform.position.y + walls.cellSize.y * 0.5f);

        // getting a reference to the MainCharacter
        player = FindObjectOfType<MainCharacter>();
    }
    void Start()
    {
        correct_Doors_Entered = 0;

        OnMapLoad();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
