using Nebula._79Nebula.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    static class Modules
    {

        public static Dictionary<MODULE, Module> Get = new Dictionary<MODULE, Module> {
            { MODULE._default, new _ExampleModule() }
        };


    }
    public enum MODULE
    {
        _default,
        attack,
        spell
    }
}
