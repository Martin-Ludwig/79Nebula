using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Interfaces;
using Nebula._79Nebula.Models;
using System.Collections.Generic;

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
            user.ApplyEffect(new EnergyPrismEffect(3));
        }
    }

    class EnergyPrismEffect : Effect, IDuration
    {
        public override string Name => "Energy Prism";
        public override string Description => $"When {Name} is applied your Defense is increased by {PlayerBase.MinorMultiplier} per Intelligence for {Duration} Rounds. When {Name} ends, you gain Barrier based on your Healing.";
        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.Buff,
            EffectType.Duration
        };
        public int Duration { get; set; }

        private int _defMod = 0;
        public EnergyPrismEffect(int duration)
        {
            Duration = duration;
        }


        public override void OnApply(Player player)
        {
            _defMod = player.Intelligence;
            player.DefenseModifier += _defMod;
        }

        public override void OnRemove(Player player)
        {
            player.DefenseModifier -= _defMod;
            player.Barrier += player.Healing;
        }

        public override void OnRoundEnd(Player player)
        {
            Duration--;
            if (Duration <= 0)
            {
                this.Remove();
            }
        }


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
        public override int GetHashCode()
        {
            return this.Duration * 17 + base.GetHashCode();
        }

    }
}
