using Nebula._79Nebula.Effects;
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
    class FastHeal : Module
    {
        public override string Name => "Minor Heal";
        public override string Description => $"Heal yourself for 75% Healing. Initiate Bonus: +50% Healing";
        public override int Priority => 2;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Spell,
            ModuleType.Heal
        };

        public override void Activate(Player user, Player opponent)
        {
            if (user.HasEffect(new InitiatorBonus()) > 0)
            {
                user.Heal(user.Healing * 1.25);
            }
            else
            {
                user.Heal(user.Healing * 0.75);
            };
        }
    }
}
