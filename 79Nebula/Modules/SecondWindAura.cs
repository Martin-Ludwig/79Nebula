using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Modules
{
    class SecondWindAura : Module
    {

        public override string Name => "Second Wind Aura";
        public override string Description => "Increases Initiative by 5. Your critical attacks and healings are increased by 2.";
        public override int Priority => 0;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Aura
        };

        public SecondWindAura()
        {
        }

        public override void Activate(Player user, Player opponent)
        {
            user.ApplyEffect(new Effects.SecondWindAura());
        }
    }

}
