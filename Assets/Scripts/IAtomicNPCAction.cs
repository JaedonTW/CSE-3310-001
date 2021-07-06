using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// An interface for atomic actions, which are meant to represent the smallest meaningful actions to be performed by NPCs
    /// </summary>
    interface IAtomicNPCAction
    {
        /// <summary>
        /// Performs the next tick action for this atomic action.
        /// </summary>
        /// <param name="actionStack"></param>
        /// <param name="c"></param>
        void ExecuteAction(Stack<IAtomicNPCAction> actionStack,MovableCharacter c);
        /// <summary>
        /// Handles a collision, this is needed because collisions will be handled differently depending on which action is currently being performed.
        /// </summary>
        /// <param name="actionStack"></param>
        /// <param name="c"></param>
        /// <param name="col"></param>
        void HandleCollision(Stack<IAtomicNPCAction> actionStack, MovableCharacter c, Collision2D col);
    }
    /// <summary>
    /// A move action for an NPC.
    /// </summary>
    class NPCMoveAction : IAtomicNPCAction
    {
        /// <summary>
        /// The destionation being travelled to.
        /// </summary>
        Vector2 Destination { get; }

        public NPCMoveAction(Vector2 destination)
        {
            Destination = destination;
        }
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, MovableCharacter c)
        {
            // current position
            var pos = c.body.position;
            var diff = new Vector2(Destination.x - pos.x, Destination.y - pos.y);
            // If the distance between 'c' and the destination is less then the distance travelled
            //   in a unit of time, then we consider this action complete and pop it from the stack.
            if (diff.sqrMagnitude < c.walkingSpeed*c.walkingSpeed)
                actionStack.Pop();
            // If 'c' is not already moving, we have it move towards the destionation.
            else if (!c.IsMoving)
                c.WalkInDirection(new Vector2(Destination.x - pos.x, Destination.y - pos.y).normalized);
            
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
