using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// An interface for atomic actions, which are meant to represent the smallest meaningful actions to be performed by NPCs
    /// </summary>
    public interface IAtomicNPCAction
    {
        /// <summary>
        /// Performs the next tick action for this atomic action.
        /// </summary>
        /// <param name="actionStack"></param>
        /// <param name="c"></param>
        void ExecuteAction(Stack<IAtomicNPCAction> actionStack, Enemy c);
        /// <summary>
        /// Handles a collision, this is needed because collisions will be handled differently depending on which action is currently being performed.
        /// </summary>
        /// <param name="actionStack"></param>
        /// <param name="c"></param>
        /// <param name="col"></param>
        void HandleCollision(Stack<IAtomicNPCAction> actionStack, Enemy c, Collision2D col);
    }
}
