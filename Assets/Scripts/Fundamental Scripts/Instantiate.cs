using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public TextBox textBox;
    Friendly friendly;

    // Spawn_Text_Box will spawn a text box above the friendly NPC. Rough idea, will adapt to static objects as well.
    public void Spawn_Text_Box()
    {
        Instantiate(textBox).transform.position = new Vector2(friendly.transform.position.x, friendly.transform.position.y + 1);
    }

    
    void Start()
    {
        // Find reference to the Friendly object.
        friendly = FindObjectOfType<Friendly>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
