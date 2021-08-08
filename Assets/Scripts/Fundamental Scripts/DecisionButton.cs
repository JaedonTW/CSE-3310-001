using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionButton : MonoBehaviour
{
    private GameManager manager;
    private MainCharacter mainCharacter;
    private MainCamera mainCamera;
    private TextBox textBox;
    private BossFightHandler _bossFightHandler;
    public GameObject CultLeader;
    
    /*
        When the user makes a decision, shrink the box
        and begin the proper course of action handled by
        the BossFightHandler.
    */
    private void OnMouseDown()
    {
        textBox.animator.SetBool("displayBox", false);
        mainCamera.StartCoroutine(mainCamera.Fade_Object());

        if (gameObject.name == "_yesButton") 
        {
            _bossFightHandler.JoinCthulhu();
        }

        else 
        {
            _bossFightHandler.RejectCthulhu();
        }
    }

    private void Start()
    {
        // Find reference to the GameManager object in the scene
        manager = FindObjectOfType<GameManager>();

        // Find reference to the MainCharacter object in the scene
        mainCharacter = FindObjectOfType<MainCharacter>();

        // Find reference to the MainCamera object in the scene
        mainCamera = FindObjectOfType<MainCamera>();

        // Find reference to the TextBox object in the scene
        textBox = GetComponentInParent<TextBox>();
        
        // Find reference to the BossFightHandler in the scene
        _bossFightHandler = FindObjectOfType<BossFightHandler>();
        
        // Start with the box displayed
        textBox.animator.SetBool("displayBox",true);
    }
}
