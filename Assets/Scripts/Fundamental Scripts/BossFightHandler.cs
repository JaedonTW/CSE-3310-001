using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class BossFightHandler : MonoBehaviour
{
    struct Wave
    {
        public int MobsterCount;
        public int CultistCount;
        public float FriendlyCultistCount;
    }
    /// <summary>
    /// Enum for each stage of the boss fight.  
    /// </summary>
    enum BossFightStage : byte
    {
        // NOTE: When modifying, it is necessary for waves 1 through x to be placed consecutively.
        PreStart = 0,
        Wave1,
        Wave2,
        Wave3,
        LevelEnd
    }
    [SerializeField]
    public Door endBossFightDoor;
    public EndLevelBox _endLevelBox;
    /// <summary>
    /// Set values for mobs in each wave.
    /// </summary>
    private Wave[] Waves { get; } = new Wave[]
    {
        new Wave()// wave 1
        {
            MobsterCount = 2,
            CultistCount = 2,
            FriendlyCultistCount = 0
        },
        new Wave()// wave 2
        {
            MobsterCount = 0,
            CultistCount = 4,
            FriendlyCultistCount = 0
        },
        new Wave()// wave 3
        {
            MobsterCount = 0,
            CultistCount = 8,
            FriendlyCultistCount = 1
        },
    };
    private BossFightStage Stage { get; set; } = BossFightStage.PreStart;
    private List<Enemy> Enemies { get; set; }
    private const int CheckEnemiesDelay = 60;
    private int TicksUntilNextCheck { get; set; } = 0;
    private void Start()
    {
        MainCharacter character = FindObjectOfType<MainCharacter>();
        if (character.Sanity > 30)
        {
            var boxes = FindObjectsOfType<TMPro.TextMeshPro>();
            foreach(var box in boxes)
                if(box.name == "_yesButton")
                {
                    Destroy(box.gameObject);
                    break;
                }
        }
    }
    // methods
    /// <summary>
    /// The method to be called if the player chooses to join Cthulhu.
    /// </summary>
    public void JoinCthulhu()
    {
        _endLevelBox.GetComponent<BoxCollider2D>().enabled = true;
        _endLevelBox.GetComponent<SpriteRenderer>().enabled = true;
        endBossFightDoor.Change_Door_State();
        Stage = BossFightStage.LevelEnd;
        _endLevelBox.nextLevel = "Bad Ending";
    }
    /// <summary>
    /// The method to be called if the player chooses not to join Cthulhu.
    /// </summary>
    public void RejectCthulhu()
    {
        StartWave(0);
        Stage = BossFightStage.Wave1;
    }
    private void StartWave(int waveIndex)
    {
        var waveSpect = Waves[waveIndex];
        Enemies = Spawner.SpawnEnemies(waveSpect.MobsterCount, waveSpect.CultistCount, waveSpect.FriendlyCultistCount, true);
    }

    private void Update()
    {
        if (Stage >= BossFightStage.Wave1 && Stage <= BossFightStage.Wave3)
        {
            if (TicksUntilNextCheck == 0)
            {
                TicksUntilNextCheck = CheckEnemiesDelay;
                for(int i = 0; i < Enemies.Count; i++)
                    if(Enemies[i] == null)
                        Enemies.RemoveAt(i--);
                if (Enemies.Count == 0)
                {
                    if (++Stage == BossFightStage.LevelEnd) 
                    {
                        _endLevelBox.GetComponent<BoxCollider2D>().enabled = true;
                        _endLevelBox.GetComponent<SpriteRenderer>().enabled = true;
                        endBossFightDoor.Change_Door_State();
                    }
                    else
                        StartWave(Stage - BossFightStage.Wave1);
                }
            }
            else
                TicksUntilNextCheck--;
        }
    }
}