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
    public class Player : PlayerCore
    {
        public int StrengthModifier { get; set; } = 0;
        public int AgilityModifier { get; set; } = 0;
        public int IntelligenceModifier { get; set; } = 0;

        public int AttackModifier { get; set; } = 0;
        public int DefenseModifier { get; set; } = 0;
        public int InitiativeModifier { get; set; } = 0;
        public int HealingModifier { get; set; } = 0;
        public int VitalityModifier { get; set; } = 0;
        public int CritBonus { get; set; } = 0;

        public new List<Module> Modules;
        private List<Effect> Effects;

        private int _barrier = 0;

        public int Damaged { get; set; } = 0;
        public int Healed { get; set; } = 0;
        public int Barrier
        {
            get { return _barrier; }
            set
            {
                if ( (_barrier + value) >= (Health / 2))
                {
                    _barrier = (Health / 2);
                } else
                {
                    _barrier += value;
                }
            }
        }

        public Player(string name, int strength, int agility, int intelligence, List<string> modules)
            : base(name, strength, agility, intelligence, modules)
        {
            // Load modules
            // Convert string to module
            try
            {
                Modules = new List<Module>();
                foreach (string moduleName in base.Modules)
                {
                    Module module = AllModules.Get(moduleName);
                    Modules.Add(module);
                }
            } catch (ModuleNotFoundException e)
            {
                throw new ModuleNotFoundException($"Could not instantiate Player({name}, {strength}/{agility}/{intelligence}, Modules: [{string.Join(",", modules)}]). \n\t{e}");
            }

            Effects = new List<Effect>();

        }

        public new int Strength
        {
            get { return base.Strength + StrengthModifier; }
        }
        public new int Agility
        {
            get { return base.Agility + AgilityModifier; }
        }
        public new int Intelligence
        {
            get { return base.Strength + IntelligenceModifier; }
        }
        public new int Attack
        {
            get { return base.Attack + AttackModifier; }
        }
        public new int Defense
        {
            get { return base.Defense + DefenseModifier; }
        }
        public new int Initiative
        {
            get { return base.Initiative + InitiativeModifier; }
        }
        public new int Healing
        {
            get { return base.Healing + HealingModifier; }
        }
        public new int Vitality
        {
            get { return base.Vitality + VitalityModifier; }
        }
        public new int Health
        {
            get { return base.Health + Healed - Damaged; }
        }

        /// <summary>
        /// Increases Barrier up to 50% of Health value
        /// </summary>
        /// <param name="n"></param>
        public void IncreaseBarrier(int n)
        {
            _barrier += n;
            if (_barrier > Health/2 )
            {
                _barrier = (int) Health/2;
            }
        }

        public int GetPriority(int i)
        {
            return Modules.ElementAt(i).Priority;
        }

        public bool ApplyEffect(Effect effect)
        {
            Effects.Add(effect);
            effect.OnApply(this);

            return true;
        }

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

        private bool isCriticalAttack(Player opponent)
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

        private bool isCriticalHealing()
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

        public int AttackPlayer(Player opponent, int increasedAttack = 0, bool isUnblockable = false)
        {
            // Todo: Trigger OnBeforePlayerAttack

            int attackDamage = Attack + increasedAttack;

            // Todo: Opponent Trigger OnBeforeDamageTaken

            if (isCriticalAttack(opponent))
            {
                attackDamage += 2;
            }
            
            int damageDealt = opponent.TakeDamage(attackDamage, isUnblockable);

            // Todo: Opponent Trigger OnAfterDamageTaken

            if (!isUnblockable && damageDealt <= 0)
            {   // Damage blocked

                // Todo: Opponent Trigger OnDamageBlocked

            }


            // Todo: Trigger OnAfterPlayerAttack

            return damageDealt;
        }

        public int TakeDamage(int damage, bool ignoreDefense = false, bool isCrit = false)
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

        public int Heal(int increasedHealing)
        {
            // Todo: Trigger OnBeforeHealing

            int gainedHealing = 0;

            if (isCriticalHealing())
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
            return $"{Name}, {Health}hp, {Strength}/{Agility}/{Intelligence}, Modules: [{string.Join(",",Modules)}]";
        }

    }
}
