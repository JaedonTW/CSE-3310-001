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
            PlannedActions.Push(new HoldRelativeDistanceAction(Manager.Player,OptimalFightDistance));
        /// <summary>
        /// Handles a change in health for a mobster.
        /// </summary>
        /// <param name="change">The desired change in health (additive).</param>
        public override void ChangeHealth(int change)
        {
            if (state == EnemyState.Guarding)
            {
                body.constraints = UnityEngine.RigidbodyConstraints2D.FreezeRotation;
                state = EnemyState.Wandering;
            }
            base.ChangeHealth(change);
        }
    }
}
