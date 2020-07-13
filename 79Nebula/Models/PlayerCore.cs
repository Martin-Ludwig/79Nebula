using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class PlayerCore
    {
        // Player's name
        public readonly string Name;

        // Strength, agility and intelligence are the main attributes a player has.
        // Base Strength (Str)
        public readonly int Strength;

        // Base Agility (Agi)
        public readonly int Agility;

        // Base Intelligence (Int)
        public readonly int Intelligence;

        // List of modules (skills) the player has.
        public readonly List<string> Modules;



        // Determines how much health one point in vitality gives.
        // Vitality = 2 * Str + Agi
        public const int VitalityMultiplier = 5;

        // Base health every player starts with.
        public const int HealthBase = 100;

        // Attack = 2 * Intelligence + Strength
        public int Attack
        {
            get { return 2 * Intelligence + Strength; }
        }

        // Defense = 2 * Agility + Strength
        public int Defense
        {
            get { return 2 * Agility + Strength; }
        }

        // Initiative = 2 * Agility + Intelligence
        public int Initiative
        {
            get { return 2 * Agility + Intelligence; }
        }

        // Healing = 2 * Intelligence + Strength
        public int Healing
        {
            get { return 2 * Intelligence + Strength; }
        }

        // Vitality = 2 * Strength + Agility
        public int Vitality
        {
            get { return 2 * Strength + Agility; }
        }

        // Health = HealthBase + Vitality * VitalityMultiplier
        public int Health
        {
            get { return HealthBase + Vitality * VitalityMultiplier; }
        }

        public PlayerCore(string name, int strength, int agility, int intelligence, List<string> modules)
        {
            Name = name;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Modules = modules;
        }

        public override string ToString()
        {
            return $"{Name}, {Health}hp, {Strength}/{Agility}/{Intelligence}, {Modules.ToString()}";
        }

    }
}
