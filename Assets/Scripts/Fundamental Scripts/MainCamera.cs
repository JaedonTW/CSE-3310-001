using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    /*
        translucent is our target world color when we
        are playing the game. non_traslucent is the color
        that will be used to fade in and out of our scenes.
        These two colors will be controlling the black_Fade
        Sprite Renderer object.
    */
    Color translucent = new Color(0, 0, 0, 0);
    Color non_translucent = new Color(0, 0, 0, 1);
    public SpriteRenderer black_Fade;

    /*
        mainCharacter, main_Camera_Position, and
        main_Character_Position will be used in this script
        have the camera track the position of the main character.
    */
    MainCharacter mainCharacter;   
    Vector3 main_Camera_Position;
    Vector3 main_Character_Position;

    /*
        Fade_To_Black will fade the camera in and out
        at different points in the game. These points include
        when you first begin a level, when you enter a new room,
        or when you enter one of the puzzle doors.
    */
    public void Fade_To_Black(/*will put bool soon, not yet though*/) 
    {
        // Still working on this
        //black_Fade.color = new Color(0, 0, 0, opacity);
    }

    void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }


    float t = 0;
    void Update()
    {
        /*
            Basic concept.
        */
        Camera.main.transform.position = new Vector3(mainCharacter.transform.position.x,mainCharacter.transform.position.y,Camera.main.transform.position.z);
        black_Fade.color = Color.Lerp(translucent,non_translucent,t);
        t += Time.deltaTime;
    }
}
