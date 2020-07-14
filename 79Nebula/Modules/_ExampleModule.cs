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
    class _ExampleModule : Module
    {
        public _ExampleModule()
        {
            Name = "Example Module";
            Priority = 0;
            ModuleType = ModuleType.Untyped;
            Description = "No description given.";
        }

        public override void Activate(Player user, Player opponent)
        {
            Console.WriteLine("Pew pew!");
            user.ModifyStrengthBy(1);
            opponent.ModifyStrengthBy(-1);
        }
    }
}
