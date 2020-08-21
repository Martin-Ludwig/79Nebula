using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Effects
{
    class EnergyBoonAura : Effect
    {
        public override string Name => "Energy Boon Aura";
        public override string Description => "Gaining and losing barrier is also applied to your health. Initiator Bonus: Gain Barrier based on your Healing.";
        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Aura,
            EffectType.Buff
        };

        public override void OnBarrierChange(Player player, ref int value)
        {
            if (value > 0)
            {
                player.GainHealth(value);
            }
            else
            {
                player.LoseHealth(value);
            }
        }

        public override void OnInitiation(Player player)
        {
            player.GainBarrier(player.Healing);
        }

    }
}
