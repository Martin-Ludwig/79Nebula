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
    class Attack : Module
    {
        public override string Name => "Attack";
        public override string Description => "Simple Attack that deals damage.";
        public override int Priority => 1;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Attack
        };

        public override void Activate(Player user, Player opponent)
        {
            user.AttackPlayer(opponent, user.Attack);
        }
    }
}
