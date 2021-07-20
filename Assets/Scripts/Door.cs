using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textMeshPro;
    protected GameManager manager;
    private MainCharacter mainCharacter;

    /*
        The following boolean will be
        used to maintain the state of the door,
        either opened or closed, which will trigger
        the proper animation to be played.
    */
    protected bool isOpen;

    /*
        Change_Door_State() will be used to set the
        correct boolean variables regarding the state
        of the door.
    */
    protected void Change_Door_State()
    {
        if (isOpen == false)
        {
            isOpen = true;
        }

        else
        {
            isOpen = false;
        }
    }

    /*
        Get_Door_State is a simple getter funciton
        that other objects can use to access the state
        of the door; opened or closed.
    */
    public bool Get_Door_State() 
    {
        return isOpen;
    }
    private void OnMouseDown()
    {
        Change_Door_State();
    }

    private void Start()
    {
        // Find reference to the GameManager object.
        manager = FindObjectOfType<GameManager>();

        // Find reference to the MainCharacter object.
        mainCharacter = FindObjectOfType<MainCharacter>();

        // The door will begin closed.
        isOpen = false;
    }

    private void Update()
    {
        // Check for animation update every frame.
        animator.SetBool("isOpen", isOpen);
        
    }
}
