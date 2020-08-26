using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Interfaces;
using Nebula._79Nebula.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using AllModules = Nebula._79Nebula.Models.Modules;

namespace Nebula._79Nebula.Models
{
    public class Player : PlayerBase
    {
        public Player(string name, int strength, int agility, int intelligence, List<string> modules)
            : base(name, strength, agility, intelligence, modules)
        {
            if (modules.Count != AutoBattle.MaxRounds)
            {   // Player equipped to much or less modules
                throw new PlayerCreationException(
                    $"Could not instantiate Player({name}, {strength}/{agility}/{intelligence}, Modules: [{string.Join(",", modules)}]). " +
                    $"The number of modules does not match the number of rounds. The player must have equipped exactly {AutoBattle.MaxRounds} modules. "
                );
            }

            try
            {
                // Load modules
                Modules = new List<Module>();
                for (int i = 0; i < AutoBattle.MaxRounds; i++) {
                    // Get module object by string name
                    Module module = AllModules.Get(modules[i]);
                    if (module.Priority >= i - 1 && module.Priority <= i + 1)
                    {
                        Modules.Add(module);
                    }
                    else
                    {   // Module's priority is out of range
                        throw new PlayerCreationException(
                            $"Could not instantiate Player({name}, {strength}/{agility}/{intelligence}, Modules: [{string.Join(",", modules)}]). " +
                            $"Priority of module #{i} (\"{modules[i]}\") is not in range. Expected priority {i - 1} between {i + 1}. "
                        );
                    }
                }

            } catch (ModuleNotFoundException e)
            {   // Module not found
                throw new PlayerCreationException($"Could not instantiate Player({name}, {strength}/{agility}/{intelligence}, Modules: [{string.Join(",", modules)}]). \n\t{e} ");
            }

        }

        public new List<Module> Modules;
        public EffectList Effects = new EffectList();

        // Stat Modifier
        public int StrengthModifier { get; set; } = 0;
        public int AgilityModifier { get; set; } = 0;
        public int IntelligenceModifier { get; set; } = 0;
        public int AttackModifier { get; set; } = 0;
        public int DefenseModifier { get; set; } = 0;
        public int InitiativeModifier { get; set; } = 0;
        public int HealingModifier { get; set; } = 0;
        public int VitalityModifier { get; set; } = 0;
        public int CritBonus { get; set; } = 0;


        // Base Stats + Modifier
        public override int Strength
        {
            get { return base.Strength + StrengthModifier; }
        }
        public override int Agility
        {
            get { return base.Agility + AgilityModifier; }
        }
        public override int Intelligence
        {
            get { return base.Intelligence + IntelligenceModifier; }
        }

        public override int Attack
        {
            get { return base.Attack + AttackModifier; }
        }
        public override int Defense
        {
            get { return base.Defense + DefenseModifier; }
        }
        public override int Initiative
        {
            get { return base.Initiative + InitiativeModifier; }
        }
        public override int Healing
        {
            get { return base.Healing + HealingModifier; }
        }
        public override int Vitality
        {
            get { return base.Vitality + VitalityModifier; }
        }

        public override int Health
        {
            get { return base.Health + Healed - Damaged; }
            set { this.HealthBase = value; }
        }

        // Health modifiers in battle
        // Combat stats to calculate health
        public int Damaged { get; set; } = 0;
        public int Healed { get; set; } = 0;

        // Barrier is reduced by damage. Only when it is at zero damage will be dealt to health. It acts like a shield.
        private int _barrier = 0;

        /// <summary>
        /// Can be up to a maximum of 50% health.
        /// </summary>
        public int Barrier
        {
            get { return _barrier; }
            set
            {
                Effects.OnBarrierChange(this, ref value);
                if ((_barrier + value) >= (Health / 2))
                {
                    _barrier = (Health / 2);
                }
                else
                {
                    _barrier = value;
                }
            }
        }


        /// <summary>
        /// Returns priority of equipped module
        /// </summary>
        public int GetModulePriority(int i)
        {
            return Modules.ElementAt(i).Priority;
        }

        /// <summary>
        /// Adds an effect to the player.
        /// </summary>
        /// <returns>True if the effect is successfully applied, else false</returns>
        public bool ApplyEffect(Effect effect)
        {
            if (effect is IStackable && this.HasEffect(effect) > 0)
            {
                Effect e = Effects.FindEffect(effect);
                e.AddStack(effect.StackSize);
            }
            else
            {
                Effects.Add(effect);
                effect.OnApply(this);
            }


            return true;
        }

        /// <summary>
        /// Removes the exact same effect
        /// </summary>
        /// <returns>True if the effect is successfully removed, else false</returns>
        public bool RemoveEffect(Effect effect)
        {
            Effect e;
            e = Effects.Find(o => o.Equals(effect) && o.IsActive);

            if (e != null)
            {
                e.Remove();
                e.OnRemove(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the first effect with the same name
        /// </summary>
        /// <returns>True if effect is successfully removed, else false</returns>
        public bool RemoveEffectByName(Effect effect)
        {
            Effect e;
            e = Effects.Find(o => o.Name.Equals(effect.Name) && o.IsActive);

            if (e != null)
            {
                e.Remove();
                e.OnRemove(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveEffectByType(EffectType effectType)
        {
            Effect e;
            e = Effects.Find(o => o.EffectTypes.Contains(effectType) && o.IsActive);

            if (e != null)
            {
                e.Remove();
                e.OnRemove(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int RemoveEffectStacks(Effect effect, int stacks)
        {
            if (effect is IStackable)
            {
                int removedStacks = 0;
                foreach (Effect e in Effects)
                {
                    if (e.Name.Equals(effect.Name) && e.IsActive)
                    {
                        removedStacks += e.RemoveStack(stacks);
                        stacks -= removedStacks;
                        if (stacks <= 0)
                        {
                            return removedStacks;
                        }
                    }
                }
                return removedStacks;
            }
            else
            {
                throw new EffectNotStackableException($"Cannot remove stacks from {effect.Name}");
            }
            
        }

        /// <summary>
        /// Counts the common occurences by comapring the name by the given effect.
        /// </summary>
        /// <returns>Returns the amount of how often the effect is applied to the player.</returns>
        public int HasEffect(Effect effect)
        {
            int n = 0;
            if (effect is IStackable)
            {
                n = Effects.Find(o => ((o.Name == effect.Name) && o.IsActive)).StackSize;
            } else
            {
                n = Effects.FindAll(o => ((o.Name == effect.Name) && o.IsActive)).Count;
            }

            return n;
        }

        private bool IsCriticalAttack(Player opponent, int critBase)
        {
            int crit = critBase;
            crit += CritBonus;

            if (Health > opponent.Health)
            {
                crit++;
            }

            if (opponent.Defense < 0)
            {
                crit++;
            }

            if (crit >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsCriticalHealing(int critBase)
        {
            int crit = critBase;
            crit += CritBonus;

            if (Healed > Damaged)
            {
                crit++;
            }

            if (Barrier == (Health/2))
            {
                crit++;
            }

            if (crit >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Attacks a player. Triggers various effects.
        /// Opponent automatically blocks the attack when you deal zero damage.
        /// </summary>
        /// <param name="opponent">Player object</param>
        /// <param name="increasedAttack">Adds increased damage</param>
        /// <param name="isUnblockable">Ignores Defense</param>
        /// <returns>Amount of damage dealt to the opponent</returns>
        public int AttackPlayer(Player opponent, double damage, bool isUnblockable = false, bool ignoreDefense = false, int critBase = 0)
        {
            // Todo: Trigger OnBeforePlayerAttack

            // Todo: Opponent Trigger OnBeforeDamageTaken

            bool isCrit = false;
            if (IsCriticalAttack(opponent, critBase))
            {
                isCrit = true;
                damage += 2;

                Effects.OnCrit(this, ref damage);
                Effects.OnCritAttack(this, ref damage, ref isUnblockable);
            }

            if (!ignoreDefense)
            {
                damage = damage * (Math.Pow(0.5, (opponent.Defense / Attack) + 0.5) + (Attack - opponent.Defense));
            }

            int damageDealt = opponent.TakeDamage(damage, isUnblockable);

            if (!isUnblockable && damageDealt <= 0)
            {   // Damage blocked

                // Todo: Opponent Trigger OnAttackBlocked(attackDamage, isCrit)
                // Todo: Player Trigger OnAttackBlockedByOpponent
            }
            else
            {
                if (isCrit)
                {
                    opponent.ApplyEffect(new Bleeding(1));
                }
            }

            // Todo: Trigger OnAfterPlayerAttack

            return damageDealt;
        }

        /// <summary>
        /// Deals damage to the player. Damage is reduced by defense.
        /// Barrier absorbs the damage before it hits the player's health.
        /// Triggers various effects.
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="ignoreDefense"></param>
        /// <returns></returns>
        public int TakeDamage(double damage = 0, bool isCrit = false)
        {

            if (isCrit)
            {
                Effects.OnIncomingCritAttack(this, ref damage);
            }

            int incomingDamage = Numbers.RoundToInt(damage);

            if (incomingDamage > 0)
            {

                if (Barrier > incomingDamage)
                {   // Barrier absorbs all the damage.
                    Barrier -= incomingDamage;

                    // Todo: Trigger OnBarrierAbsorbsFullDamage

                }
                else if (Barrier > 0)
                {   // Barrier absorbs a portion of the damage.
                    incomingDamage -= Barrier;
                    Barrier = 0;

                    // Todo: Trigger OnBarrierDestroyed

                }

                // Deal damage to player.
                LoseHealth(incomingDamage);
            }
            else
            {
                // Make sure it is not negative.
                incomingDamage = 0;
            }


            // Todo: Trigger OnAfterDamageTaken

            return incomingDamage;
        }

        /// <summary>
        /// Heals the player's health.
        /// </summary>
        /// <param name="increasedHealing">Amount of íncreased healing</param>
        /// <returns>Amount of how much the player was healed.</returns>
        public double Heal(double amount, int critBase = 0)
        {
            // Todo: Trigger OnBeforeHealing

            if (IsCriticalHealing(critBase))
            {
                ApplyEffect(new Regeneration(1));
                amount += 2;

                Effects.OnCrit(this, ref amount);
                Effects.OnCritHeal(this, ref amount);
            }


            int finalHeal = Numbers.RoundToInt(amount);

            GainHealth(finalHeal);

            // Todo: Trigger OnAfterHealing

            return finalHeal;
        }

        /// <summary>
        /// Player directly loses health without triggering any actions. Ignores Defense and Barrier.
        /// </summary>
        public void LoseHealth(double n)
        {
            Damaged += Numbers.RoundToInt(n);
        }

        /// <summary>
        /// Player directly gains health without triggering any actions.
        /// </summary>
        public void GainHealth(double n)
        {
            Healed += Numbers.RoundToInt(n);
        }

        /// <summary>
        /// Increases Player's Barrier.
        /// </summary>
        public void GainBarrier(double n)
        {
            Effects.OnBarrerGain(this, ref n);

            Barrier += Numbers.RoundToInt(n);
        }

        /// <summary>
        /// Resets Health, Modifiers and Effects back to default.
        /// </summary>
        public void Reset()
        {
            Damaged = 0;
            Healed = 0;
            Effects.Clear();
            StrengthModifier = 0;
            AgilityModifier = 0;
            IntelligenceModifier = 0;
            AttackModifier = 0;
            DefenseModifier = 0;
            HealingModifier = 0;
            InitiativeModifier = 0;
            VitalityModifier = 0;
            CritBonus = 0;
        }
        public override string ToString()
        {
            string modules = "";
            foreach (Module m in Modules)
            {
                modules += m.Name + ", ";
            }
            modules = modules.Substring(0, modules.Length - 2);


            return $"{Name}, {Health}hp, {Strength}/{Agility}/{Intelligence}, Modules: [{modules}]";
        }

    }
}
