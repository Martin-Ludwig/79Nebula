using Nebula._79Nebula.Enums;
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
    class Bleeding : Effect, IStackable
    {

        public override string Name => "Bleeding";
        public override string Description => $"{Name} deals {_base} + Attack * {_coefficient} damage at the end of every round.";
        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Debuff,
            EffectType.Condition
        };

        private const int _base = PlayerBase.MajorMultiplier + PlayerBase.MinorMultiplier;
        private const double _coefficient = 0.125;

        public Bleeding(int stacks = 1)
        {
            AddStack(stacks);
        }

        public override void OnRoundEnd(Player player)
        {
            double damage = _base + player.Attack * _coefficient;
            player.LoseHealth(damage * StackSize);
        }

    }
}
