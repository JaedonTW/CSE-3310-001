using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    /*
        player_Position_Screen represents the
        player's position in screen space instead
        of world space. At this moment, I will not
        include a variable to hold the players
        position in world space as it can be 
        easily accessed at any time without using
        a predefined variable.
    */
    protected Vector2 player_Screen_Position;
    /*
     * Speed is multiplied by the direction
     * to give the character velocity. The bigger it is, 
     * the faster the character goes. [SerializeField] 
     * allows us to adjust this in the inspector.
     */
    [SerializeField] public float Speed;


    /*
        get_Player_Position() is a getter function
        that will allow other objects to reference 
        the player's on-screen position, without having access
        to the player_Position_Screen variable.
    */
    public Vector2 get_Player_Screen_Position()
    {
        player_Screen_Position = Camera.main.WorldToScreenPoint(transform.position);
        return player_Screen_Position;
    }

    /*
        The on-screen joystick that our
        player will be utilizing.
    */
    protected Joystick joystick;

/*
    animator is the Animator utlitiy
    attached to our object.
*/
    public Animator animator;


    private void Start()
    {
        /*
            Referencing the Animator component
            attached to the object
        */
        animator = GetComponent<Animator>();

        /*
            Because there only exists one joystick, we 
            can easily use FindObjectOfType<>() to locate
            it without dragging and dropping its instance.
        */
        joystick = FindObjectOfType<Joystick>();
        
        /*
            AxisOption allows us to enable use on the
            horizontal, vertical, or both axis. I enabled
            both for now.
        */
        joystick.AxisOptions = AxisOptions.Both;
        
        /*
            joystick.SnapX and joystick.SnapY ensure that the values
            of joystick_Horizontal and joystick_Vertical are 0, 1, or -1.
        */
        joystick.SnapX = true;
        joystick.SnapY = true;

        /*
            Initializing our character's velocity in either
            direction. This can also be changed from the inspector.
        */
        Speed = 1;
    }

    protected void Move_Player()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(joystick.Horizontal * Speed, joystick.Vertical * Speed);
    }

    void Update()
    {
        animator.SetFloat("joystick_Horizontal", joystick.Horizontal);
        animator.SetFloat("joystick_Vertical", joystick.Vertical);
        Move_Player();
    }
}
