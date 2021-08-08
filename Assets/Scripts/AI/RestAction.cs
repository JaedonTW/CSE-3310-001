using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    class RestAction : IAtomicNPCAction
    {
        int Ticks { get; set; }
        /// <summary>
        /// Instantiates a 'RestAction'
        /// </summary>
        /// <param name="ticks">The desired number of ticks to rest.</param>
        public RestAction(int ticks)
        {
            Ticks = ticks;
        }
        /// <summary>
        /// Executes the next iteration of this action.
        /// </summary>
        /// <param name="actionStack">The current action stack.</param>
        /// <param name="c">A reference to the enemy being controlled.</param>
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, Enemy c)
        {
            if (c.IsMoving)
                c.SetIdle();
            if (--Ticks == 0)
                // removing 
                actionStack.Pop();
        }
        /// <summary>
        /// Handles a collision event.
        /// </summary>
        /// <param name="actionStack">The current action stack.</param>
        /// <param name="thisNPC">The enemy being controlled.</param>
        /// <param name="col">The collision object.</param>
        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, Enemy c, Collision2D col)
        {
            // cancel rest action and let main AI handle this
            actionStack.Pop();
        }
    }
}
