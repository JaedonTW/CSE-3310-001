using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    class Mobster : Enemy
    {
        internal override float OptimalFightDistance => 3f;
        protected override void InitializeAttack() =>
            PlannedActions.Push(new HoldRelativeDistanceAction(Manager.player,OptimalFightDistance));
    }
}
