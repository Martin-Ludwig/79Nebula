using Nebula._79Nebula.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nebula._79Nebula.Models
{
    public abstract class Effect
    {
        public virtual string Name => "Default Effect";
        public virtual string Description => "No description given.";
        public virtual List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.untyped
        };

        private EffectState State { get; set; } = EffectState.Active;

        public bool IsActive { get { return State.Equals(EffectState.Active); } }

        public void Remove()
        {
            State = EffectState.Removed;
        }

        public Effect()
        {
        }

        /// <summary>
        /// Triggers when this effect is applied to a player.
        /// </summary>
        public virtual void OnApply(Player player) { }

        /// <summary>
        /// Triggers when this effect is removed from a player.
        /// </summary>
        public virtual void OnRemove(Player player) { }

        public virtual void OnRoundEnd(Player player) { }

        public override string ToString()
        {
            return $"{Name}, Type: {EffectTypes}\n\t{Description}";
        }

        public override bool Equals(object obj)
        {
            Effect other = (Effect)obj;

            if (other == null)
            {
                return false;
            }

            if (!this.Name.Equals(other.Name)) {
                return false;
            }

            if (!this.Description.Equals(other.Description))
            {
                return false;
            }

            if (!this.EffectTypes.SequenceEqual(other.EffectTypes))
            {
                return false;
            }

            if (!this.IsActive.Equals(other.IsActive))
            {
                return false;
            }


            return true;
        }

        public override int GetHashCode()
        {
            int hash1 = this.EffectTypes.Sum(e => Convert.ToInt32(e)) * 1999 + this.EffectTypes.Count + (int) State;

            return hash1 * 37 + this.Name.GetHashCode();
        }

        private enum EffectState
        {
            Active,
            Removed
        }

    }


}
