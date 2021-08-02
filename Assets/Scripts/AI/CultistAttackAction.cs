using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    class CultistAttackAction : IAtomicNPCAction
    {
        MovableCharacter Reference { get; }
        Vector2 Displacement { get; }
        
        public CultistAttackAction(MovableCharacter reference, Vector2 displacement)
        {
            Reference = reference;
            Displacement = displacement;
        }
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, Enemy c)
        {
            if (c.state != EnemyState.HasLineOfSight)
            {
                actionStack.Pop();
                c.Manager.CultistCoordinator.LeaveAttack((Cultist)c);
            }
            else
            {
                var path = Reference.body.position + Displacement - c.body.position;
                if (path.sqrMagnitude < 0.01)
                    c.SetIdle();
                else
                    c.WalkInDirection(path.normalized);
            }
        }

        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, Enemy c, Collision2D col)
        {
            actionStack.Push(new GoToPositionAction(Reference.body.position + Displacement));
        }
    }
}
