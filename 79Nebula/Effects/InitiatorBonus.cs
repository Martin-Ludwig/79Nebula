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

        public InitiatorBonus()
            : base(
                  "Initiator Bonus",
                  "Increases Strength, Agility and Intelligence by 1.",
                  new List<EffectType> { 
                      EffectType.Buff 
                  }
            )
        {

        }

        public new void OnApply(Player player)
        {
            player.ModifyStrengthBy(1);
            player.ModifyAgilityBy(1);
            player.ModifyIntelligenceBy(1);
        }

        public new void OnRemove(Player player)
        {
            player.ModifyStrengthBy(-1);
            player.ModifyAgilityBy(-1);
            player.ModifyIntelligenceBy(-1);
        }

    }
}
