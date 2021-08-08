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
            //actionStack.Push(new GoToPositionAction(Reference.body.position + Displacement));
            float x = 0, y = 0;
            foreach (var enemy in Enemy.Enemies)
            {
                if (enemy == null)
                    Debug.LogWarning("A value from \"Enemy.Enemies\" was null.");
                else if (c != enemy)
                {
                    var disp = enemy.body.position - c.body.position;
                    if (disp == Vector2.zero)
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
