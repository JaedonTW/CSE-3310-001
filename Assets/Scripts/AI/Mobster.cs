using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    class Mobster : Enemy
    {
        protected override void InitializeAttack() =>
            PlannedActions.Push(new HoldRelativeDistanceAction(Manager.player,optimalFightDistance));
    }
}
