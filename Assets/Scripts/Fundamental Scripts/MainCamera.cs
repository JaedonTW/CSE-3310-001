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
    public IEnumerator Fade_Black(bool isOpen) 
    {
        // Lock cursor if not already so the user cannot click a door before fade has completed
        Cursor.lockState = CursorLockMode.Locked;
        
        // Fade_Interval signifies the 2 seconds it takes the camera to fade in/out of black
        float Fade_Interval= 2f;
        
        // Elapsed_Time signifies how much time has passed; will update each frame
        float Elapsed_Time = 0f;
        
        // If you need to fade out, run the following conditional
        if (isOpen == true) 
        {
            while (Elapsed_Time < Fade_Interval)
            {
                black_Fade.color = Color.Lerp(translucent, non_translucent, Elapsed_Time);
                Elapsed_Time = Elapsed_Time + Time.deltaTime;
                yield return null;
            }
        }

        // Else, fade in
        while (Elapsed_Time < Fade_Interval)
        {
            black_Fade.color = Color.Lerp(non_translucent, translucent, Elapsed_Time);
            Elapsed_Time = Elapsed_Time + Time.deltaTime;
            yield return null;
        }

        // Unlock cursor so the user can proceed with the game
        Cursor.lockState = CursorLockMode.None;
    }

    public IEnumerator Fade_Object(bool isOpen, Color objColor)
    {
        //Color currentColor = (objC);

        // Fade_Interval signifies the 2 seconds it takes the camera to fade in/out of black
        float Fade_Interval = 2f;

        // Elapsed_Time signifies how much time has passed; will update each frame
        float Elapsed_Time = 0f;

        // If you need to fade out, run the following conditional
        if (isOpen == true)
        {
            while (Elapsed_Time < Fade_Interval)
            {
                objColor = Color.Lerp(translucent, non_translucent, Elapsed_Time);
                Elapsed_Time = Elapsed_Time + Time.deltaTime;
                yield return null;
            }
        }

        // Else, fade in
        while (Elapsed_Time < Fade_Interval)
        {
            objColor = Color.Lerp(non_translucent, translucent, Elapsed_Time);
            Elapsed_Time = Elapsed_Time + Time.deltaTime;
            yield return null;
        }
    }

    void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }
    
    void Update()
    {
        // Follow main character
        Camera.main.transform.position = new Vector3(mainCharacter.transform.position.x,mainCharacter.transform.position.y,Camera.main.transform.position.z);
    }
}
