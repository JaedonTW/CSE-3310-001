using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The class that corrosponds to the main character object.
/// </summary>
public class MainCharacter : MovableCharacter
{
    /// <summary>
    /// Joystick to be used to control the movement of the main character.
    /// </summary>
    protected Joystick joystick;
    /// <summary>
    /// Transformation for the main camera.
    /// </summary>
    protected Transform cam;

    protected override void Start()
    {
        base.Start();
        // Getthing the joystick and camera objects
        joystick = FindObjectOfType<Joystick>();
        cam = FindObjectOfType<Camera>().transform;
        // setting the camera to be focused on the MainCharacter (player)
        cam.position = new Vector3(body.position.x, body.position.y, cam.position.z);
    }
    protected override void Update()
    {
        // should this be done in start?
        joystick.SnapX = true;
        joystick.SnapY = true;
        // updating player movement.
        Vector2 traveling = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (traveling.sqrMagnitude != 0)
            WalkInDirection(traveling);
        else SetIdle();
        // calling update for parent object.
        // this is done after getting user input to improve response time.
        base.Update();
        // moving the camera to keep up with the MainCharacter (player).
        cam.position = new Vector3(body.position.x, body.position.y, cam.position.z);
    }
}
