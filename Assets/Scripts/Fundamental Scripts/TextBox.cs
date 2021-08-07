using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextBox : MonoBehaviour
{
    public Animator animator;
    public TMP_Text text;
    public Door front_Door;
    private GameManager manager;
    private MainCharacter mainCharacter;
    public GameObject bookShelf;

    

    /*
        Logic handles displaying and hiding the Display Box
        which promopts the user to continue into the castle as
        there is a storm outside.
    */
    public bool displayBox;

    private void _displayRiddle() 
    {
        if(manager.Check_Distance(mainCharacter.transform,bookShelf.transform) == true) 
        {
            displayBox = true;
            animator.SetBool("displayBox", displayBox);
        }

        else 
        {
            displayBox = false;
            animator.SetBool("displayBox", displayBox);
        }
    }


    /*protected void Check_Front_Door()
    {
        if (front_Door.tag.CompareTo("Front_Door") == 0 && front_Door.Get_Door_State() == true && manager.Check_Distance(mainCharacter.transform, front_Door.transform))
        {
            animator.SetBool("displayBox", true);
        }

        else if (front_Door.tag.CompareTo("Front_Door") == 0 && front_Door.Get_Door_State() == false || !manager.Check_Distance(mainCharacter.transform, front_Door.transform))
        {
            animator.SetBool("displayBox", false);
        }
    }*/

    private void Start()
    {
        displayBox = false;
        animator.SetBool("displayBox", displayBox);
        
        // Find reference to MainCharacter object in the scene.
        mainCharacter = FindObjectOfType<MainCharacter>();
        
        // Find reference to Manager object in the scene.
        manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name.CompareTo("Level3 - Boss") != 0) 
        {
            _displayRiddle();
        }
        
    }
}
