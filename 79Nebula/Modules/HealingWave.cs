using Nebula._79Nebula.Effects;
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
    class HealingWave : Module
    {
        public override string Name => "Healing Wave";
        public override string Description => $"Heals yourself. Increased Healing for each Regeneration. Removes 2 Conditions.";
        public override int Priority => 4;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Spell,
            ModuleType.Heal
        };

        public override void Activate(Player user, Player opponent)
        {
            double heal = user.Healing + user.HasEffect(new Regeneration()) * 0.125;
            user.Heal(heal);

            user.RemoveEffectByType(EffectType.Condition);
            user.RemoveEffectByType(EffectType.Condition);
        }
    }
}
