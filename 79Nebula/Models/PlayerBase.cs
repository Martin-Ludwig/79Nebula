using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class PlayerBase
    {
        // Player's name
        public readonly string Name;

        // Strength, agility and intelligence are the main attributes a player has.
        
        // Base Strength (Str)
        // increased vitality by 2 and attack, defense and healing by 1
        public virtual int Strength { get; }

        // Base Agility (Agi)
        // increases initiative and defense by 2 and vitality by 1
        public virtual int Agility { get; }

        // Base Intelligence (Int)
        // increases attack and healing by 2 and initiative by 1
        public virtual int Intelligence { get; }

        // List of modules (skills) the player has.
        public readonly List<string> Modules;

        // Coefficient of the primary attribute value.
        public const int MajorMultiplier = 2;

        // Coefficient of the secondary attribute value.
        public const int MinorMultiplier = 1;

        // Determines how much health one point in vitality gives.
        // Vitality = 2 * Str + Agi
        public const int VitalityMultiplier = 5;

        // Base health every player starts with.
        public int HealthBase = 100;

        // Attack = 2 * Intelligence + Strength
        public virtual int Attack
        {
            get { return MajorMultiplier * Intelligence + MinorMultiplier * Strength; }
        }

        // Defense = 2 * Agility + Strength
        public virtual int Defense
        {
            get { return MajorMultiplier * Agility + MinorMultiplier * Strength; }
        }

        // Initiative = 2 * Agility + Intelligence
        public virtual int Initiative
        {
            get { return MajorMultiplier * Agility + MinorMultiplier * Intelligence; }
        }

        // Healing = 2 * Intelligence + Strength
        public virtual int Healing
        {
            get { return MajorMultiplier * Intelligence + MinorMultiplier * Strength; }
        }

        // Vitality = 2 * Strength + Agility
        public virtual int Vitality
        {
            get { return MajorMultiplier * Strength + MinorMultiplier * Agility; }
        }

        // Health = HealthBase + Vitality * VitalityMultiplier
        public virtual int Health
        {
            get { return HealthBase + Vitality * VitalityMultiplier; }
            set { this.HealthBase = value; }
        }

        public PlayerBase(string name, int strength, int agility, int intelligence, List<string> modules)
        {
            Name = name;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Modules = modules;
        }

        public override string ToString()
        {
            return $"{Name}, {Health}hp, {Strength}/{Agility}/{Intelligence}, {Modules}";
        }

    }
}
