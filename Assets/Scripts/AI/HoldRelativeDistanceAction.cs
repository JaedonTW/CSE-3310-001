using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// When is beyond a certain distance from a specified 'MovableCharacter', 
    ///   moves towards that object.  Terminates when no longer in 'LineOfSight' state
    /// </summary>
    class HoldRelativeDistanceAction : IAtomicNPCAction
    {
        MovableCharacter Reference { get; }
        float DistanceSquared { get; }
        public HoldRelativeDistanceAction(MovableCharacter reference, float distance)
        {
            Reference = reference;
            DistanceSquared = distance * distance;
        }

        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, Enemy c)
        {
            if (c.State != EnemyState.HasLineOfSight)
                actionStack.Pop();
            else
            {
                var curr = (c.body.position - Reference.body.position).sqrMagnitude;
                if (curr > DistanceSquared)
                    c.WalkInDirection((Reference.body.position - c.body.position).normalized);
                else
                    c.SetIdle();
            }
        }

        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, Enemy c, Collision2D col)
        {
            // In the case of a collision, we do nothing.
        }
    }
}
