using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public MainCharacter player;
    public Friendly[] friendlies;
    public int CurrentLevel { get; private set; } = 0;
    
    /*
        correct_Doors_Entered will hold the number of
        doors entered in a row by the user. The user 
        will need to enter 4 correct doors in a row to 
        finish the level.
    */
    private int correct_Doors_Entered;
    
    

    
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
        bool found = false;

        foreach(KeyValuePair<string,int> s in door_Order) 
        { 
            if(door_Name == s.Key && correct_Doors_Entered == s.Value && found == false) 
            {
                correct_Doors_Entered++;
                found = true;
            }
        }

        /*
            If the user enters the wrong door, 
            the correct_Door_Counter starts back
            at 0.
        */
        if(found == false) 
        {
            correct_Doors_Entered = 0;
        }

        /*
            If all correct doors were entered,
            you may pass to the next stage.
        */
        if(correct_Doors_Entered == 5) 
        {
            Debug.Log("YOU WIN !!!\n");
        }
    }

    void Start()
    {
        correct_Doors_Entered = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
