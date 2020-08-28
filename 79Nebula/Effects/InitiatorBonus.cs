using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Effects
{
    public class InitiatorBonus : Effect
    {
        public override string Name => "Initiator Bonus";
        public override string Description => "Increases Strength, Agility and Intelligence by 1.";
        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Buff
        };

        public override void OnApply(Player player)
        {
            player.StrengthModifier++;
            player.AgilityModifier++;
            player.IntelligenceModifier++;
            player.CritBonus++;
            player.BlockBonus++;
        }

        public override void OnRemove(Player player)
        {
            player.StrengthModifier--;
            player.AgilityModifier--;
            player.IntelligenceModifier--;
            player.CritBonus--;
            player.BlockBonus--;
        }
    }
}
