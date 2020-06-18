using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public abstract class Module
    {
        public string Name { get; set; }

        ///The lower the number, the more priority the skill has.
        public int Priority { get; set; }
        public ModuleType ModuleType { get; set; }
        public string Description { get; set; }

        // construct is used by the inherited classes.
        public Module()
        {

        }

        /// <summary>
        /// Function behind the module.
        /// </summary>
        public abstract void Activate(Player user, Player opponent);


        public override string ToString()
        {
            return $"{Name} ({Priority}), Type: {ModuleType}\n\t{Description}";
        }

    }
}
