using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    class GoToPositionStreamAction : GoToPositionAction
    {
        MovableCharacter Following { get; }
        internal override Vector2 Destination => Following.body.position;
        /// <summary>
        /// Executes the next iteration of this action.
        /// </summary>
        /// <param name="actionStack">The current action stack.</param>
        /// <param name="c">A reference to the enemy being controlled.</param>
        public GoToPositionStreamAction(MovableCharacter following)
        {
            Following = following;
        }
    }
}
