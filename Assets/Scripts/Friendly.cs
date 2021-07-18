using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Friendly : NPC
{
    /*
        Below are the floating point values used 
        to store the main character's position, as
        well as the friednly NPC's position. x_Val
        and y_Val represent the displacement between
        the friendly and the main character in each
        direction.
    */
    float friendly_XPos;
    float friendly_YPOS;
    float mainCharacter_XPos;
    float mainCharacter_YPos;
    float x_Val;
    float y_Val;

    /*
        instantiation_Distance is the furthest distance
        the main character can be from a friendly before 
        a pop-up box is instantiated.
    */
    [SerializeField] float instantiation_Distance;

    protected MainCharacter mainCharacter;
    private Instantiate instantiate;
    
    void Start()
    {
        // initialize instantiation_Distance
        instantiation_Distance = 1.75f;
        
        // Find the reference to the instantiate object
        instantiate = FindObjectOfType<Instantiate>();
        
        // Find the reference to the mainCharacter object
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        Check_Distance();
    }

    private void OnMouseDown()
    {   
    }

    /*
        Check_Distance uses the famed Distance Formula
        to check the distance between the Friendly NPC
        and the mainCharacter. If the distance is less
        than or equal to 5 world units, the Pop-up
        Text Box will spawn.
    */
    public void Check_Distance()
    {
        friendly_XPos = gameObject.transform.position.x;
        friendly_YPOS = gameObject.transform.position.y;
        mainCharacter_XPos = mainCharacter.transform.position.x;
        mainCharacter_YPos = mainCharacter.transform.position.y;

        x_Val = Mathf.Sqrt(Mathf.Pow(friendly_XPos - mainCharacter_XPos, 2));
        y_Val = Mathf.Sqrt(Mathf.Pow(friendly_YPOS - mainCharacter_YPos, 2));

        if (Mathf.Sqrt(x_Val + y_Val) <= instantiation_Distance)
        {
            instantiate.Spawn_Text_Box();
            Debug.Log("Close Close Close");
        }
    }
}
