using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
    public MainCharacter Player;
    private MainCamera _mainCamera;
    internal Inkie inkiePrefab;

    private ParticleSystem[] particleSystem;

    private EndLevelBox _endLevelBox;
    private TextBoxMessage _textBoxMessage;

    [SerializeField]
    public Tilemap Walls { get; private set; }
    public CultistManager CultistCoordinator { get; set; } = new CultistManager();
    public int EnemyCount { get; set; }
    
    /*
        correct_Doors_Entered will hold the number of
        doors entered in the correct order by the user. 
        The user will need to enter 5 correct doors in a row to 
        finish the level.
    */
    private int correct_Doors_Entered;
    
    /*
        GetCorrectDoorsEntered() is a getter function that 
        returns that number of correct doors entered by the user.
    */
    public int GetCorrectDoorsEntered() 
    {
        return correct_Doors_Entered;
    }

    /*
        Check_Distance uses the famed Distance Formula
        to check the distance between two entities. 
        If the distance is less than or equal to 5 world units, 
        the Pop-up Text Box will spawn.
    */
    public bool Check_Distance(Transform obj1, Transform obj2)
    {
        float obj1_XPos = obj1.transform.position.x;
        float obj1_YPos = obj1.transform.position.y;
        float obj2_XPos = obj2.transform.position.x;
        float obj2_YPos = obj2.transform.position.y;

        float x_Val = Mathf.Sqrt(Mathf.Pow(obj1_XPos - obj2_XPos, 2));
        float y_Val = Mathf.Sqrt(Mathf.Pow(obj1_YPos - obj2_YPos, 2));
        float instantiation_Distance = 1.5f;

        if (Mathf.Sqrt(x_Val + y_Val) <= instantiation_Distance)
        {
            return true;
        }

        return false;
    }

    public void StartLevel(int level)
    {
        throw new System.NotImplementedException();
    }
    public void ToMainMenu()
    {
        throw new System.NotImplementedException();
    }

    /*
        Track_Door_Order will keep track of if the user entered the correct
        door in the correct order. A Disctionary lookup will be utilized to
        ensure that the Name of the Door and it's value in the door order
        match.
    */
    public void Track_Door_Order(SortedDictionary<string,int> door_Order, string door_Name) 
    {   
        // We check if the door entered was the correct one. 
        if (door_Order[door_Name] == correct_Doors_Entered)
        {
            // If the door enterd was correct, we increment,
            // update the Puzzle Door Text fields, and check if
            // all of the correct doors were entered.
            correct_Doors_Entered++;
            _updatePuzzleDoorText();
            
            if(correct_Doors_Entered == 5) 
            {
                // If all correct doors were entered,
                // you may pass to the next stage.
                _endLevelBox.GetComponent<BoxCollider2D>().enabled = true;
                _endLevelBox.GetComponent<SpriteRenderer>().enabled = true;
                Trigger_Confetti();
            }
        }
        
        else 
        {
            // If the user enters the wrong door, 
            // the correct_Door_Counter starts back at 0.
            // Update the Puzzle Door Text back to the first riddle.
            correct_Doors_Entered = 0;
            _updatePuzzleDoorText();
        }
           
    }
    
    /*
        _updatePuzzleDoorText will fill in the text fields 
        of the text box depending on how many doors have
        been entered in the correct order.
    */
    private void _updatePuzzleDoorText() 
    {
        // If you have not yet finished the puzzle, update the Puzzle Doors.
        if(correct_Doors_Entered < 5) 
        {
            _textBoxMessage.Set_Current_Riddles();
            _textBoxMessage._loadTextBox();
        }
    }
    
    void OnMapLoad()
    {
        // initializing the 'PathMap' and the 'MapOffset'
        // getting walls
        var tilemaps = FindObjectsOfType<Tilemap>();
        Tilemap walls = null;
        foreach(var tilemap in tilemaps)
            if(tilemap.name == "Walls")
            {
                walls = tilemap;
                break;
            }
        if (walls == null)
            throw new MissingComponentException("There must be a 'Tilemap' component named 'Walls'.");
        Walls = walls;
        //
        var config = FindObjectOfType<MapConfiguration>();
        if (config == null)
            Debug.LogWarning("Missing 'MapConfiguration' object.");
        else
        {
            Spawner.SpawnEnemies(config);
        }
        // getting a reference to the MainCharacter
        Player = FindObjectOfType<MainCharacter>();
        Player.Manager = this;
    }

    void Trigger_Confetti()
    {
        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Play();
        }
    }

    void Start()
    {
        // Find reference to the TextBoxMessage object in the scene.
        _textBoxMessage = FindObjectOfType<TextBoxMessage>();

        // Find reference to the EndLevelBox object in the scene.
        _endLevelBox = FindObjectOfType<EndLevelBox>();

        // Find reference to MainCamera object in the scene.
        _mainCamera = FindObjectOfType<MainCamera>();
        
        if (SceneManager.GetActiveScene().name != "Level1 - Mansion")
        {
            // Disable the box collider and the sprite renderer at the beggining of the level
            _endLevelBox.GetComponent<BoxCollider2D>().enabled = false;
            _endLevelBox.GetComponent<SpriteRenderer>().enabled = false;
        }
        
        // The user has not entered any correct doors at the start of the level.
        correct_Doors_Entered = 0;

        // Find reference to the ParticleSystem objects.
        particleSystem = FindObjectsOfType<ParticleSystem>();

        _mainCamera.StartCoroutine(_mainCamera.Fade_Black(false));

        OnMapLoad();
    }

    void Update()
    {
        CultistCoordinator.Tick();
    }
}
