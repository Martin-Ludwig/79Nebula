using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Extensions;
using Nebula._79Nebula.Interfaces;
using Nebula._79Nebula.Models;
using Nebula._79Nebula.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Effects
{
    class Regeneration : Effect, IStackable
    {
        public override string Name => "Regeneration";
        public override string Description => $"{Name} heals you for {_baseHealing} + Healing * {_coefficient} at the end of every round.";
        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Buff,
            EffectType.Boon
        };

        private const int _baseHealing = PlayerBase.MajorMultiplier + PlayerBase.MinorMultiplier;
        private const double _coefficient = 0.125;

        public Regeneration(int stacks = 1)
        {
            this.AddStack(stacks);
        }


        public override void OnRoundEnd(Player player)
        {
            int healing = _baseHealing + Numbers.RoundToInt(player.Healing * _coefficient);
            player.GainHealth(healing);
        }

    }
}
