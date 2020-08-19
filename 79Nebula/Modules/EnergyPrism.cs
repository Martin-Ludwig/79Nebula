using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Modules
{
    class EnergyPrism : Module
    {
        public override string Name => "Energy Prism";
        public override string Description => $"For 3 Rounds you gain {PlayerBase.MinorMultiplier} Defense per Intelligence. When {Name} ends, you gain Barrier based on your Healing.";
        public override int Priority => 2;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Spell,
            ModuleType.Duration
        };

        public override void Activate(Player user, Player opponent)
        {
            user.ApplyEffect(new EnergyPrismEffect());
        }
    }

    class EnergyPrismEffect : Effect
    {

        public override string Name => "Energy Prism";

        public override string Description => $"{Name} increases your Defense by {PlayerBase.MinorMultiplier} per Intelligence. When {Name} ends, you gain Barrier based on your Healing.";

        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Buff
        };

        public override void OnApply(Player player)
        {
            base.OnApply(player);
        }

        public override void OnRemove(Player player)
        {
            base.OnRemove(player);
        }


    }
}
