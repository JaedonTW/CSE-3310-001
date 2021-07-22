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
        public GoToPositionStreamAction(MovableCharacter following)
        {
            Following = following;
        }
    }
}
