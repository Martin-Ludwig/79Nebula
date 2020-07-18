using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class Modules
    {

        private static readonly Dictionary<MODULE, Module> _modules = new Dictionary<MODULE, Module> {
            { MODULE.Default0, new DefaultModule0() },
            { MODULE.Default1, new DefaultModule1() },
            { MODULE.Default2, new DefaultModule2() },
            { MODULE.Default3, new DefaultModule3() },
            { MODULE.Default4, new DefaultModule4() },
            { MODULE.Default5, new DefaultModule5() },
            { MODULE.Default6, new DefaultModule6() }
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
            bool isModule = Enum.TryParse<MODULE>(name, out MODULE module);

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
        Default0 = 0,
        Default1 = 1,
        Default2 = 2,
        Default3 = 3,
        Default4 = 4,
        Default5 = 5,
        Default6 = 6,
    }
}
