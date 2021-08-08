using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionButton : MonoBehaviour
{
    private BossFightHandler _bossFightHandler;
    private MainCharacter mainCharacter;
    private TextBox textBox;
    private GameManager manager;
    public GameObject CultLeader;
    bool choiceMade;
    MainCamera mainCamera;
    private void OnMouseDown()
    {
        choiceMade = true;
        textBox.animator.SetBool("displayBox", false);

        if (gameObject.name == "_yesButton") 
        {
            _bossFightHandler.JoinCthulhu();
            mainCamera.Fade_Object(false, CultLeader.GetComponent<SpriteRenderer>().color);
            return;
        }

        mainCamera.Fade_Object(true, CultLeader.GetComponent<Color>());
        _bossFightHandler.RejectCthulhu();
    }

    private void Start()
    {
        choiceMade = false;
        manager = FindObjectOfType<GameManager>();
        _bossFightHandler = FindObjectOfType<BossFightHandler>();
        textBox = GetComponentInParent<TextBox>();
        mainCharacter = FindObjectOfType<MainCharacter>();
        mainCamera = FindObjectOfType<MainCamera>();
        //textBox.animator.SetBool("displayBox", false);
        
    }

    void BoxDecision() 
    {
        if (manager.Check_Distance(mainCharacter.transform, CultLeader.transform) == true && choiceMade == false)
        {
            textBox.animator.SetBool("displayBox", true);
        }

        else
        {
            textBox.animator.SetBool("displayBox", false);
        }
    }

    private void Update()
    {
        BoxDecision();
    }
}
