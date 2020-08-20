﻿using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Interfaces;
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
            // Todo: Testing
            // Applies Energy Prism Effect for 3 Rounds.
            user.ApplyEffect(new EnergyPrismEffect(3));
        }
    }

    class EnergyPrismEffect : Effect, IDuration
    {

        public override string Name => "Energy Prism";

        public override string Description => $"{Name} increases your Defense by {PlayerBase.MinorMultiplier} per Intelligence. When {Name} ends, you gain Barrier based on your Healing.";

        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Buff,
            EffectType.Duration
        };

        public int Duration { get; set; }

        public EnergyPrismEffect(int duration)
        {
            Duration = duration;
        }
        
        public override void OnApply(Player player)
        {
            base.OnApply(player);
        }

        public override void OnRemove(Player player)
        {
            base.OnRemove(player);
        }

        public override void OnRoundEnd(Player player)
        {
            Duration--;
            if (Duration <= 0)
            {

            }

            // Todo: Testing
            player.RemoveEffect(this);
        }


        // Todo: Testing
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }

            var other = obj as EnergyPrismEffect;

            if (!this.Duration.Equals(other.Duration))
            {
                return false;
            }

            return true;
        }

        // Todo: Testing
        public override int GetHashCode()
        {
            return this.Duration * 17 + base.GetHashCode();
        }

    }
}
