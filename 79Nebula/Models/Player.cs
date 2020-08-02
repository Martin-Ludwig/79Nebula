using Nebula._79Nebula.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    if (module.Priority >= i-1 && module.Priority <= i+1)
                    {
                        Modules.Add(module);
                    }
                    else
                    {   // Module's priority is out of range
                        throw new PlayerCreationException(
                            $"Could not instantiate Player({name}, {strength}/{agility}/{intelligence}, Modules: [{string.Join(",", modules)}]). " +
                            $"Priority of module #{i} (\"{modules[i]}\") is not in range. Expected priority {i-1} between {i+1}. "
                        );
                    }
                }

            } catch (ModuleNotFoundException e)
            {   // Module not found
                throw new PlayerCreationException($"Could not instantiate Player({name}, {strength}/{agility}/{intelligence}, Modules: [{string.Join(",", modules)}]). \n\t{e} ");
            }

        }

        public new List<Module> Modules;
        private readonly List<Effect> Effects = new List<Effect>();

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
                if ((_barrier + value) >= (Health / 2))
                {
                    _barrier = (Health / 2);
                }
                else
                {
                    _barrier += value;
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
        public bool ApplyEffect(Effect effect)
        {
            Effects.Add(effect);
            effect.OnApply(this);

            return true;
        }

        /// <summary>
        /// Remove an effect by object (checks name)
        /// </summary>
        /// <returns>True if effect is successfully removed, else false</returns>
        public bool RemoveEffect(Effect effect)
        {
            Effect e;
            e = Effects.Find(o => o.Name == effect.Name);

            if (e != null)
            {
                e.OnRemove(this);
                Effects.Remove(e);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes an effect by its hashcode.
        /// </summary>
        /// <returns>True if effect is successfully removed, else false</returns>
        public bool RemoveEffect(int hashcode)
        {
            if (Effects.RemoveAll(o => o.GetHashCode() == hashcode) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the amount of how often the effect is applied to the player.
        /// </summary>
        public int HasEffect(Effect effect)
        {
            int n = Effects.FindAll(o => o.Name == effect.Name).Count;

            return n;
        }

        private bool IsCriticalAttack(Player opponent)
        {
            int crit = 0;
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

        private bool IsCriticalHealing()
        {
            int crit = 0;
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
        public int AttackPlayer(Player opponent, int increasedAttack = 0, bool isUnblockable = false)
        {
            // Todo: Trigger OnBeforePlayerAttack

            int attackDamage = Attack + increasedAttack;

            // Todo: Opponent Trigger OnBeforeDamageTaken

            bool isCrit = false;
            if (IsCriticalAttack(opponent))
            {
                isCrit = true;
                attackDamage += 2;
            }
            
            int damageDealt = opponent.TakeDamage(attackDamage, isUnblockable);

            // Todo: Opponent Trigger OnAfterDamageTaken

            if (!isUnblockable && damageDealt <= 0)
            {   // Damage blocked

                // Todo: Opponent Trigger OnAttackBlocked(attackDamage, isCrit)
                // Todo: Player Trigger OnAttackBlockedByOpponent
            }
            else
            {
                if (isCrit)
                {
                    // Todo: bleeding on crit
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
        public int TakeDamage(int damage, bool ignoreDefense = false)
        {
            int damageTaken;
            if (ignoreDefense)
            {
                damageTaken = damage;
            }
            else
            {
                damageTaken = damage - Defense;
            }

            if (damageTaken > 0)
            {

                if (Barrier > damageTaken)
                {   // Barrier absorbs all the damage.
                    Barrier -= damageTaken;

                    // Todo: Trigger OnBarrierAbsorbsFullDamage

                }
                else if (Barrier > 0)
                {   // Barrier absorbs a portion of the damage.
                    damageTaken -= Barrier;
                    Barrier = 0;

                    // Todo: Trigger OnBarrierDestroyed

                }

                // Deal damage to player.
                LoseHealth(damageTaken);
            }
            else
            {
                // Make sure it is not negative.
                damageTaken = 0;
            }

            return damageTaken;
        }

        /// <summary>
        /// Heals the player's health.
        /// </summary>
        /// <param name="increasedHealing">Amount of íncreased healing</param>
        /// <returns>Amount of how much the player was healed.</returns>
        public int Heal(int increasedHealing)
        {
            // Todo: Trigger OnBeforeHealing

            int gainedHealing = 0;

            if (IsCriticalHealing())
            {
                gainedHealing += 2;
            }

            gainedHealing += Healing + increasedHealing;

            GainHealth(gainedHealing);

            // Todo: Trigger OnAfterHealing

            return gainedHealing;
        }

        /// <summary>
        /// Player directly loses health without triggering any actions. Ignores Defense and Barrier.
        /// </summary>
        public void LoseHealth(int n)
        {
            Damaged += n;
        }

        /// <summary>
        /// Player directly gains health without triggering any actions. Ignores Healing attriute.
        /// </summary>
        public void GainHealth(int n)
        {
            Healed += n;
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
