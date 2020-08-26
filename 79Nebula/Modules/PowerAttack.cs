using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Modules
{
    class PowerAttack : Module
    {
        public override string Name => "Power Attack";
        public override string Description => "Strong Attack that deals double damage.";
        public override int Priority => 3;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Attack
        };

        private const double _coefficient = 2;

        public override void Activate(Player user, Player opponent)
        {
            double attack = _coefficient * user.Attack;
            user.AttackPlayer(opponent, attack);
        }
    }
}
