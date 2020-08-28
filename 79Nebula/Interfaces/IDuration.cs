using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Interfaces
{
    public interface IDuration
    {
        public int Duration { get; set; }

    }

    public static class DurationExtension
    {
        public static void DurationExpire(this IDuration obj, Effect effect)
        {
            obj.Duration--;

            if (obj.Duration <= 0)
            {
                effect.Remove();
            }
        }
    }
}
