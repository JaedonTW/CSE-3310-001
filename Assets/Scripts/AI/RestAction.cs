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
        public RestAction(int ticks)
        {
            Ticks = ticks;
        }
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, Enemy c)
        {
            if (c.IsMoving)
                c.SetIdle();
            if (--Ticks == 0)
                // removing 
                actionStack.Pop();
        }

        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, Enemy c, Collision2D col)
        {
            // cancel rest action and let main AI handle this
            actionStack.Pop();
        }
    }
}
