using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// The class that corrosponds to the main character object.
/// </summary>
public class MainCharacter : MovableCharacter
{
    public List<Weapon> CurrentWeapons { get; set; } = new List<Weapon>();
    /// <summary>
    /// mainCharacter will have sanity which depends
    /// on if friendlies are saved/killed.
    /// </summary>
    [SerializeField] protected int insanity;

    /// <summary>
    /// Getter function for insanity.
    /// </summary>
    public int Get_Insanity() 
    {
        return insanity;
    }

    /// <summary>
    /// Joystick to be used to control the movement of the main character.
    /// </summary>
    protected Joystick joystick;
    /// <summary>
    /// Transformation for the main camera.
    /// </summary>
    protected Camera cam;
    
    protected override void Start()
    {
        // Setting insanity to 50 for example
        insanity = 50;
        base.Start();
        DamageGroup = DamegeGroups.Player;

        // Getting the joystick and camera objects
        joystick = FindObjectOfType<Joystick>();
        cam = FindObjectOfType<Camera>();
        // setting the camera to be focused on the MainCharacter (player)
        cam.transform.position = new Vector3(body.position.x, body.position.y, cam.transform.position.z);
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
        cam.transform.position = new Vector3(body.position.x, body.position.y, cam.transform.position.z);

        // dealing with user input
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Mouse0 has been pressed.
            if(weapon != null && !EventSystem.current.IsPointerOverGameObject())
            {
                // Mouse is not over a game object such as the joystick.
                var pos = Input.mousePosition;
                var playerScreenPos = cam.WorldToScreenPoint(body.transform.position);
                var angle = Mathf.Atan2(pos.y - playerScreenPos.y, pos.x - playerScreenPos.x);
                weapon.Use(angle);
            }
        }
        else
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                // checking if is over game object
                if (!EventSystem.current.IsPointerOverGameObject(t.fingerId))
                {
                    print("Is not in a bad place!");
                    break;
                }
                else
                    print("Is in bad place...");
            }
    
        
    
    
    }


}
