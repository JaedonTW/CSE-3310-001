using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    interface IAtomicNPCAction
    {
        void ExecuteAction(Stack<IAtomicNPCAction> actionStack,MovableCharacter c);
        void HandleCollision(Stack<IAtomicNPCAction> actionStack, MovableCharacter c, Collision2D col);
    }
    class NPCMoveAction : IAtomicNPCAction
    {
        Vector2 Destination { get; }

        public NPCMoveAction(Vector2 destination)
        {
            Destination = destination;
        }
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, MovableCharacter c)
        {
            var pos = c.body.position;
            var diff = new Vector2(Destination.x - pos.x, Destination.y - pos.y);
            if(diff.sqrMagnitude < c.walkingSpeed*c.walkingSpeed)
                actionStack.Pop();
            else if (!c.IsMoving)
                c.WalkInDirection(new Vector2(Destination.x - pos.x, Destination.y - pos.y));
            
        }

        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, MovableCharacter movable, Collision2D col)
        {
            actionStack.Pop();
        }
    }
    class NPCDelayAction : IAtomicNPCAction
    {
        int Ticks { get; set; }
        public NPCDelayAction(int ticks)
        {
            Ticks = ticks;
        }
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, MovableCharacter c)
        {
            if (c.IsMoving)
                c.SetIdle();
            if (--Ticks == 0)
                actionStack.Pop();
        }

        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, MovableCharacter c, Collision2D col)
        {
            
        }
    }
}
