using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Interfaces;
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

        public int StackSize { get; set; } = 0;

        public Effect()
        {
        }

        /// <summary>
        /// Adds stacks to effect.
        /// </summary>
        public void AddStack(int n = 1)
        {
            if (this is IStackable)
            {
                StackSize += n;
            } else
            {
                throw new EffectNotStackableException($"{Name} is not stackable.");
            }
        }

        /// <summary>
        /// Removes stacks. Returns the amount of stacks that were removed.
        /// </summary>
        public int RemoveStack(int n = 1)
        {
            if (this is IStackable)
            {
                StackSize -= n;

                if (StackSize <= 0)
                {
                    int amountRemoved = StackSize + n;

                    StackSize = 0;
                    this.Remove();

                    return amountRemoved;
                } else
                {
                    return n;
                }
            }
            else
            {
                throw new EffectNotStackableException($"{Name} is not stackable.");
            }
        }


        /// <summary>
        /// Triggers when this effect is applied to a player.
        /// </summary>
        public virtual void OnApply(Player player) { }

        /// <summary>
        /// Triggers when this effect is removed from a player.
        /// </summary>
        public virtual void OnRemove(Player player) { }

        /// <summary>
        /// Triggers at the start of each round.
        /// </summary>
        public virtual void OnRoundStart(Player player) { }
        /// <summary>
        /// Triggers at the end of each round.
        /// </summary>
        public virtual void OnRoundEnd(Player player) { }

        /// <summary>
        /// Player executes a critical action. This Trigger combines CritHeal and CritAttack.
        /// </summary>
        /// <param name="player">User</param>
        /// <param name="valueOut">Outgoing Damage/Healing</param>
        public virtual void OnCrit(Player player, ref int valueOut) { }

        /// <summary>
        /// Player deals a critical attack.
        /// </summary>
        /// <param name="player">User</param>
        /// <param name="damageOut">Power of your attack. (Before opponent defenses)</param>
        /// <param name="isUnblockable">Whether opponent can block your attack.</param>
        public virtual void OnCritAttack(Player player, ref int damageOut, ref bool isUnblockable) { }

        /// <summary>
        /// Player receives a critical healing.
        /// </summary>
        /// <param name="player">User</param>
        /// <param name="healingOut">Power of your healing.</param>
        public virtual void OnCritHeal(Player player, ref int healingOut) { }

        /// <summary>
        /// Opponent deals a critical attack to you.
        /// </summary>
        /// <param name="player">User</param>
        /// <param name="damageIn">Value of health that you will lose.</param>
        public virtual void OnIncomingCritAttack(Player player, ref int damageIn) { }

        /// <summary>
        /// Player gains Barrier.
        /// </summary>
        /// <param name="player">User</param>
        /// <param name="barrierIn">Amount of Barrier you gain.</param>
        public virtual void OnBarrerGain(Player player, ref int barrierIn) { }

        /// <summary>
        /// Player gained or lost Barrier.
        /// </summary>
        /// <param name="player">User</param>
        /// <param name="value">Amount of change</param>
        public virtual void OnBarrierChange(Player player, ref int value) { }

        /// <summary>
        /// Triggers when the player is faster than the opponent and begins the round.
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnInitiation(Player player) { }


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
