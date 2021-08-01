using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{
    public Animator animator;
    public TMP_Text text;
    public Door front_Door;
    private GameManager manager;
    private MainCharacter mainCharacter;



    /*
        Logic handles displaying and hiding the Display Box
        which promopts the user to continue into the castle as
        there is a storm outside.
    */
    public bool displayBox;
    protected void Check_Front_Door()
    {
        if (front_Door.tag.CompareTo("Front_Door") == 0 && front_Door.Get_Door_State() == true && manager.Check_Distance(mainCharacter.transform, front_Door.transform))
        {
            animator.SetBool("displayBox", true);
        }

        else if (front_Door.tag.CompareTo("Front_Door") == 0 && front_Door.Get_Door_State() == false || !manager.Check_Distance(mainCharacter.transform, front_Door.transform))
        {
            animator.SetBool("displayBox", false);
        }
    }

    private void Start()
    {
        /*
            Ill comment tomorrow .....
        */
        animator.SetBool("displayBox", displayBox);
        mainCharacter = FindObjectOfType<MainCharacter>();
        manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Check_Front_Door();
    }
}
