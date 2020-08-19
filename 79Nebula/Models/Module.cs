using Nebula._79Nebula.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public abstract class Module
    {
        public virtual string Name => "Example Module";
        ///The lower the number, the higher priority the skill has.
        public virtual int Priority => 0;
        public virtual List<ModuleType> ModuleTypes => new List<ModuleType>()
                { ModuleType.Untyped };
        public virtual string Description => "No description given.";

        public Module()
        {
        }

        /// <summary>
        /// Unique function behind the module.
        /// </summary>
        public abstract void Activate(Player user, Player opponent);


        public override string ToString()
        {
            return $"{Name} ({Priority}), Type: [{ string.Join(",", ModuleTypes)}]\n\t{Description}";
        }

    }
}
