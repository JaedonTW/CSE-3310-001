using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    /*
        door_Order is a SortedDictionary that we will use to
        determine the order in which Harris must enter the doors
        to finish level 2. 
    */
    SortedDictionary<string, int> door_Order = new SortedDictionary<string, int>()
    {
        {"Viper",0},
        {"Reed",1},
        {"Water Ripple",2},
        {"Vulture",3},
        {"Lion", 4}
    };
    
    public Animator animator;
    public TextMeshProUGUI textMeshPro;
    private GameManager manager;

    /*
        The following boolean will be
        used to maintain the state of the door,
        either opened or closed, which will trigger
        the proper animation to be played.
    */
    private bool isOpen;

    /*
        Change_Door_State() will be used to set the
        correct boolean variables regarding the state
        of the door.
    */
    void Change_Door_State() 
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

    private void OnMouseDown()
    {
        Change_Door_State();

        /*
            The gameObject's tag is the name of
            the door defined in the inspector.
        */
        manager.Track_Door_Order(door_Order,gameObject.tag);

        if(isOpen == true) 
        {
            //textMeshPro.GetComponent<TextMeshProUGUI>().text = gameObject.tag + " is opened!";
            Debug.Log("OPENING!\n");
        }

        else
        {
            Debug.Log("CLOSING\n");
            //textMeshPro.GetComponent<TextMeshProUGUI>().text = gameObject.tag + " is closed!";
        }
    }

    
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        /*
            The door will start off
            closed.
        */
        isOpen = false;

    }

    
    void Update()
    {
        animator.SetBool("isOpen",isOpen);
    }
}
