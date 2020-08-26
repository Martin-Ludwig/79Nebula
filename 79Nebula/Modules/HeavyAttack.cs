using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using Nebula._79Nebula.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Modules
{
    class HeavyAttack : Module
    {
        public override string Name => "Heavy Attack";
        public override string Description => "Heavy Attack that deals 350% more damage.";
        public override int Priority => 5;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Attack
        };

        private const double _coefficient = 3.5;

        public override void Activate(Player user, Player opponent)
        {
            double attack = _coefficient * user.Attack;
            user.AttackPlayer(opponent, attack);
        }
    }
}
