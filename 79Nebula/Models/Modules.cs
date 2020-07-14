using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public static class Modules
    {

        private static Dictionary<MODULE, Module> _modules = new Dictionary<MODULE, Module> {
            { MODULE.Default, new _ExampleModule() }
        };


        public static Module Get(MODULE module)
        {
            try
            {
                return _modules[module];
            }
            catch (Exception)
            {
                throw new ModuleNotFoundException($"Module \"{module}\" not found.");
            }
        }
        public static Module Get(int id)
        {
            try
            {
                MODULE module = (MODULE)id;
                return _modules[module];
            }
            catch (Exception)
            {
                throw new ModuleNotFoundException($"Module with id {id} not found.");
            }

        }
        public static Module Get(string name)
        {
            MODULE module;
            bool isModule = Enum.TryParse<MODULE>(name, out module);

            if (isModule && Enum.IsDefined(typeof(MODULE), module))
            {
                return _modules[module];
            }
            else
            {
                throw new ModuleNotFoundException($"Module \"{name}\" not found.");
            }
        }

    }
    public enum MODULE
    {
        Default = 0,
        Attack = 10,
        Spell,
        A = 20,
        B,
    }
}
