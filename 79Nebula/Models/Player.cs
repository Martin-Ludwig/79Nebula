using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class Player : PlayerCore
    {
        private int _strengthModifier = 0;

        public Player(string name, int strength, int agility, int intelligence, List<string> skills)
            : base(name, strength, agility, intelligence, skills)
        {
            
        }

        public int Strength
        {
            get { return base.Strength + _strengthModifier; }
        }

        public void ModifyStrengthBy(int n)
        {
            _strengthModifier += n;
        }


        public override string ToString()
        {
            return $"{Name}, {Health}hp, {Strength}/{Agility}/{Intelligence}, {Modules.ToString()}";
        }

    }
}
