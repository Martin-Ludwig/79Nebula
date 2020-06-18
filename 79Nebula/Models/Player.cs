using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class Player : PlayerCore
    {
        public Player(string name, int strength, int agility, int intelligence, List<string> skills)
            : base(name, strength, agility, intelligence, skills)
        {
            
        }

    }
}
