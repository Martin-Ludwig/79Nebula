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
    class DefaultModule2 : Module
    {
        public override string Name => "Default Module 2";
        public override string Description => "No description given.";
        public override int Priority => 2;
        public override List<ModuleType> ModuleTypes => new List<ModuleType>(){
            ModuleType.Untyped
        };

        public override void Activate(Player user, Player opponent)
        {
            Console.WriteLine("Pew pew!");
        }
    }
}
