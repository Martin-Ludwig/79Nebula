using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Effects
{
    public class SecondWindAura : Effect
    {
        public override string Name => "Second Wind Aura";
        public override string Description => "Increases Initiative by 5. Your critical attacks and healings are increased by 2.";
        public override List<EffectType> EffectTypes => new List<EffectType>(){ 
            EffectType.Aura, 
            EffectType.Buff 
        };
        public SecondWindAura()
        {
        }

        public override void OnApply(Player player)
        {
            player.InitiativeModifier += 5;
        }

        // Todo: OnCrit()

    }
}
