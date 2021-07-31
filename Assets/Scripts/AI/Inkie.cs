﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    class Inkie : Mobster
    {
        public override void OnDeath()
        {
            InkSpawner.CurrentInkCount--;
            base.OnDeath();
        }
    }
}