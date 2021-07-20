using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    class Cultist : Enemy
    {
        protected override void Start()
        {
            base.Start();
            Manager.CultistCoordinator.RegisterCultist(this);
        }
        protected override void InitializeAttack()
        {
            Manager.CultistCoordinator.JoinAttack(this);
        }
    }
}
