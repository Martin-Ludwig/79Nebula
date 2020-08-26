using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using Nebula._79Nebula.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Modules
{
    class MinorHeal : Module
    {
        public override string Name => "Minor Heal";
        public override string Description => $"Heals yourself for 30% Healing and gain Barrier from 70% Healing.";
        public override int Priority => 1;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Spell,
            ModuleType.Heal
        };

        public override void Activate(Player user, Player opponent)
        {
            user.Heal(user.Healing * 0.3);
            user.GainBarrier(user.Healing * 0.7);
        }
    }
}
