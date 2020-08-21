using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using Nebula._79Nebula.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Effects
{
    class Regeneration : Effect
    {
        public override string Name => "Regeneration";
        public override string Description => $"{Name} heals you for {_baseHealing} + Healing * {_coefficient} at the end of every round.";
        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Buff
        };

        private const int _baseHealing = 2;
        private const double _coefficient = 0.2;

        public override void OnRoundEnd(Player player)
        {
            int healing = _baseHealing + Numbers.RoundToInt(player.Healing * _coefficient);
            player.GainHealth(healing);
        }

    }
}
