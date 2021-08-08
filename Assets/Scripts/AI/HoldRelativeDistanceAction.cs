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
            if (c.state != EnemyState.HasLineOfSight)
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
            Debug.Log("HoldRelativeDistanceAction: Handling Collision");
            float x = 0, y = 0;
            foreach (var enemy in Enemy.Enemies)
            {
                if(enemy == null)
                    Debug.LogWarning("A value from \"Enemy.Enemies\" was null.");
                else if (c != enemy)
                {
                    var disp = enemy.body.position - c.body.position;
                    if(disp == Vector2.zero)
                    {
                        Debug.LogWarning("There are two enemies with the same coordinates.");
                        continue;
                    }
                    float weight = 1 / disp.sqrMagnitude;
                    x -= disp.x * weight;
                    y -= disp.y * weight;
                }
            }
            const float scale = 0.1f;
            actionStack.Push(new GoToPositionAction(c.body.position + new Vector2(x * scale, y * scale)));
        }
    }
}
