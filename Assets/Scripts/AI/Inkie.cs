using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    class Inkie : Mobster
    {
        /// <summary>
        /// Handles the death of an 'Inkie'
        /// </summary>
        public override void OnDeath()
        {
            InkSpawner.CurrentInkCount--;
            base.OnDeath();
        }
    }
}
