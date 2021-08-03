using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxMessage : MonoBehaviour
{
    GameManager manager;

    private TextBox[] textBoxes;
    /*
        The below string Lists are the door riddles
        that the user will use to complete the 
        Level 2 puzzle.
    */
    private List<string> _viperRiddles = new List<string>
    {
        "I CREEP AND I CRAWL",
        "I AM THE CAUSE OF THE FALL",
        "I START REAL SMALL",
        "I CAN GROW TO BE QUITE TALL",
        "I AM QUITE DECEPTIVE", "MAY DANGER IS NOT SUBJECTIVE",
        "IF YOU SEE ME, HAVE CAUTION",
        "SO YOU WON'T END UP IN A COFFIN"
    };
    private List<string> _lionRiddles = new List<string>
    {
        "I PARADE MY DWELLING",
        "WITH AUTHORITY AND MIGHT",
        "PLEASE DO NOT COME NEAR",
        "I'LL TRY NOT TO BITE",
        "I COME TO LEAD",
        "NEVER TO FOLLOW",
        "I STAY WITH A TIGHT PACK",
        "THAT NEVER GETS HOLLOW"
    };
    private List<string> _lightningRiddles = new List<string>
    {
        "I PACK A REAL PUNCH, BUT I'M REAL LIGHT",
        "WITH ME, THERE IS NO FIGHT",
        "I CAN BRING FIRE",
        "I CAN EVEN BRING SIGHT",
        "I LIVE HIGH IN THE SKY",
        "WAY ABOVE THE NESTS",
        "I'LL TRY NOT TO TOUCH YOU",
        "HOPEFULLY YOU WON'T BE NEXT"
    };
    private List<string> _fireRiddles = new List<string>
    {
        "I'M BLUE BUT NOT COLD",
        "I'M YELLOW BUT NOT GOLD",
        "YOU CAN BRING ME ANYWHERE YOU'D LIKE",
        "I COME IN A PACKAGE, BUT NOT SOLD",
        "I LOVE TO DANCE AROUND IN THE DARK",
        "I CAN PINCH LIKE A DART",
        "TREAT ME RIGHT",
        "AND I'LL CONTINUE TO BEAT LIKE A HEART"
    };
    private List<string> _waterRiddles = new List<string>
    {
        "I GO WITH THE FLOW",
        "IN ME, MANY ROW",
        "SOME MAY GO FAST",
        "OTHERS MAY GO SLOW",
        "I CAN BE AS SMALL AS A PINKY",
        "OR AS BIG AS THE MISSISSIPPI",
        "DON'T LEAVE ME IN THE HEAT",
        "OR I'LL LEAVE REAL QUICKLY"
    };
    private List<List<string>> riddleArray;

    private void _loadTextBox() 
    {
        textBoxes = FindObjectsOfType<TextBox>();

        for(int i=0; i<textBoxes.Length; i++) 
        {
            TextMesh textMesh = textBoxes[i].GetComponentInChildren<TextMesh>();
            textMesh.text = _viperRiddles[i];
        }
    }
    
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();

        /*
            Loading the riddleArray
        */
        riddleArray = new List<List<string>> { _viperRiddles,_lionRiddles,_lightningRiddles,_fireRiddles, _waterRiddles};
    }

    private void Update()
    {
        //if()
        
    }
}
