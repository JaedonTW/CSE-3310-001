using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

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
        LevelEnd,
    }
    [SerializeField]
    private Door endBossFightDoor;
    /// <summary>
    /// Set values for mobs in each wave.
    /// </summary>
    private Wave[] Waves { get; } = new Wave[]
    {
        new Wave()// wave 1
        {
            MobsterCount = 10,
            CultistCount = 10,
            FriendlyCultistCount = 0.5f
        },
        new Wave()// wave 2
        {
            MobsterCount = 10,
            CultistCount = 10,
            FriendlyCultistCount = 0.5f
        },
        new Wave()// wave 3
        {
            MobsterCount = 10,
            CultistCount = 10,
            FriendlyCultistCount = 0.5f
        },
    };
    private BossFightStage Stage { get; set; } = BossFightStage.PreStart;
    private List<Enemy> Enemies { get; set; }
    private const int CheckEnemiesDelay = 60;
    private int TicksUntilNextCheck { get; set; } = 0;
    // methods
    /// <summary>
    /// The method to be called if the player chooses to join Cthulhu.
    /// </summary>
    public void JoinCthulhu()
    {
        endBossFightDoor.Change_Door_State();
        Stage = BossFightStage.LevelEnd;
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
                        endBossFightDoor.Change_Door_State();
                    else
                        StartWave(Stage - BossFightStage.Wave1);
                }
            }
            else
                TicksUntilNextCheck--;
        }
    }
}