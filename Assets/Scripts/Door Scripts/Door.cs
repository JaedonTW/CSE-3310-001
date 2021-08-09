using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    /// <summary>
    /// Animator for this door.
    /// </summary>
    public Animator animator;
    protected GameManager manager;
    protected MainCharacter mainCharacter;
    protected MainCamera mainCamera;

    /*
        The following boolean will be
        used to maintain the state of the door,
        either opened or closed, which will trigger
        the proper animation to be played.
    */
    protected bool isOpen;

    /// <summary>
    /// Change_Door_State() will be used to set the
    /// correct boolean variables regarding the state
    /// of the door. Rather than having a conditional,
    /// we can use the unary not operator to change the
    /// state to it's opposite boolean value.
    /// </summary>
    public void Change_Door_State()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
    
    /// <summary>
    /// Get_Door_State is a simple getter funciton
    /// that other objects can use to access the state
    /// of the door; opened or closed.
    /// </summary>
    /// <returns></returns>
    public bool Get_Door_State() 
    {
        return isOpen;
    }

    private void OnMouseDown()
    {
        Change_Door_State();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(SceneManager.GetActiveScene().name == "Level3 - Boss" && collision.gameObject.tag == "Player" && isOpen) 
        {
            SceneManager.LoadScene(4);
        }
    }

    private void Start()
    {
        // Find reference to the GameManager object.
        manager = FindObjectOfType<GameManager>();

        // Find reference to the MainCharacter object.
        mainCharacter = FindObjectOfType<MainCharacter>();

        // Find reference to the MainCamera object.
        mainCamera = FindObjectOfType<MainCamera>();

        // The door will begin in the closed state.
        animator.SetBool("isOpen", false);
    }
}
