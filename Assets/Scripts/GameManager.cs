using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HUD hud;
    public Enemy[] enemies;
    public MainCharacter player;
    public Friendly[] friendlies;
    public int CurrentLevel { get; private set; } = 0;
    public void StartLevel(int level)
    {
        throw new System.NotImplementedException();
    }
    public void ToMainMenu()
    {
        throw new System.NotImplementedException();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hud.setData(player);
    }
}
