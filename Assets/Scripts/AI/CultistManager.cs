using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class CultistManager
    {
        const int TeleportCooldownTicks = 60*20;
        const float ProbabilityOfSuccessfulTeleport = 0.25f;
        int TeleportCooldownTicksRemaining { get; set; }
        float AngleOffset { get; set; } = 0;
        List<Cultist> AttackingCultists { get; set; } = new List<Cultist>();
        List<Cultist> Cultists { get; set; } = new List<Cultist>();
        internal void RegisterCultist(Cultist cultist)
        {
            Cultists.Add(cultist);
        }
        void ResetPositions()
        {
            var theta = AngleOffset;
            var dtheta = Mathf.PI * 2 / AttackingCultists.Count;
            for (int i = 0; i < AttackingCultists.Count; i++)
            {
                var c = AttackingCultists[i];
                c.PlannedActions.Clear();
                c.PlannedActions.Push(new CultistAttackAction(c.Manager.Player,
                    new Vector2(Mathf.Cos(theta) * c.OptimalFightDistance, Mathf.Sin(theta) * c.OptimalFightDistance)
                    ));
                theta += dtheta;
            }
        }
        internal void JoinAttack(Cultist cultist)
        {
            if (AttackingCultists.Count == 0)
            {
                TeleportCooldownTicksRemaining = TeleportCooldownTicks;
                // initializing offset
                var dx = cultist.Manager.Player.body.position - cultist.body.position;
                AngleOffset = Mathf.Atan2(dx.y,dx.x);
                
                // having every cultist move towards the player
                foreach(var c in Cultists)
                    if(c != cultist)
                    {
                        c.PlannedActions.Clear();
                        c.PlannedActions.Push(new GoToPositionStreamAction(cultist.Manager.Player));
                    }
            }
            AttackingCultists.Add(cultist);
            ResetPositions();
        }
        internal void LeaveAttack(Cultist cultist)
        {
            AttackingCultists.Remove(cultist);
            if (AttackingCultists.Count > 0)
            {
                cultist.PlannedActions.Clear();
                cultist.PlannedActions.Push(new GoToPositionStreamAction(cultist.Manager.Player));
                ResetPositions();
            }
            else
                foreach (var c in Cultists)
                {
                    c.PlannedActions.Clear();
                    c.PlannedActions.Push(new GoToPositionAction(cultist.Manager.Player.body.position));
                }
        }
        internal void Tick()
        {
            if (TeleportCooldownTicksRemaining == 0 && AttackingCultists.Count > 0)
            {
                foreach (var c in Cultists)
                    if (!AttackingCultists.Contains(c))
                    {
                        TeleportCooldownTicksRemaining = TeleportCooldownTicks;
                        if (UnityEngine.Random.Range(0, 1) < ProbabilityOfSuccessfulTeleport)
                        {
                            MonoBehaviour.print("Teleporting...");
                            c.transform.position = c.Manager.Player.transform.position;
                        }
                        break;
                    }
            }
            else
                TeleportCooldownTicksRemaining--;
        }
    }
}
