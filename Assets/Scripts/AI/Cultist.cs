using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    class Cultist : Enemy
    {
        internal override float OptimalFightDistance => 1f;
        protected override void Start()
        {
            base.Start();
            Manager.CultistCoordinator.RegisterCultist(this);
            weapon = Instantiate(weapon);
            weapon.body = body;
        }
        protected override void InitializeAttack() =>
            Manager.CultistCoordinator.JoinAttack(this);
    }
}
