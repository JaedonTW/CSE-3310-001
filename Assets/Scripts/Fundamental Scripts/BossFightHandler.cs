using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BossFightHandler : MonoBehaviour
{
    struct Wave
    {
        public int MobsterCount;
        public int CultistCount;
        public float FriendlyCultistCount;
    }
    enum BossFightStage : byte
    {
        PreStart = 0,
        Wave1,
        Wave2,
        Wave3,
        LevelEnd,
    }
    [SerializeField]
    private Door endBossFightDoor;
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
    // methods
    public void JoinCthulhu()
    {
        Stage = BossFightStage.LevelEnd;
        
    }
    public void RejectCthulhu()
    {

    }
}