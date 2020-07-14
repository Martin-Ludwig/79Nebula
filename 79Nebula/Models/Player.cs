﻿using Nebula._79Nebula.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllModules = Nebula._79Nebula.Models.Modules;

namespace Nebula._79Nebula.Models
{
    public class Player : PlayerCore
    {
        private int _strengthModifier = 0;
        private int _agilityModifier = 0;
        private int _intelligenceModifier = 0;

        private int _attackModifier = 0;
        private int _defenseModifier = 0;
        private int _initiativeModifier = 0;
        private int _healingModifier = 0;
        private int _vitalityModifier = 0;

        public new List<Module> Modules;

        private List<Effect> Effects;

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
            get { return base.Strength + _strengthModifier; }
        }
        public new int Agility
        {
            get { return base.Agility + _agilityModifier; }
        }
        public new int Intelligence
        {
            get { return base.Strength + _intelligenceModifier; }
        }
        public new int Attack
        {
            get { return base.Attack + _attackModifier; }
        }
        public new int Defense
        {
            get { return base.Defense + _defenseModifier; }
        }
        public new int Initiative
        {
            get { return base.Initiative + _initiativeModifier; }
        }
        public new int Healing
        {
            get { return base.Healing + _healingModifier; }
        }
        public new int Vitality
        {
            get { return base.Vitality + _vitalityModifier; }
        }

        public void ModifyStrengthBy(int n)
        {
            _strengthModifier += n;
        }
        public void ModifyAgilityBy(int n)
        {
            _agilityModifier += n;
        }
        public void ModifyIntelligenceBy(int n)
        {
            _intelligenceModifier += n;
        }
        public void ModifyAttackBy(int n)
        {
            _attackModifier += n;
        }
        public void ModifyDefenseBy(int n)
        {
            _defenseModifier += n;
        }
        public void ModifyInitiativeBy(int n)
        {
            _initiativeModifier += n;
        }
        public void ModifyHealingBy(int n)
        {
            _healingModifier += n;
        }
        public void ModifyVitalityBy(int n)
        {
            _vitalityModifier += n;
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

        public override string ToString()
        {
            return $"{Name}, {Health}hp, {Strength}/{Agility}/{Intelligence}, Modules: [{string.Join(",",Modules)}]";
        }

    }
}
