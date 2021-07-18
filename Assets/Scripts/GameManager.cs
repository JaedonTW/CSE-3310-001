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

    void Start()
    {
        correct_Doors_Entered = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
