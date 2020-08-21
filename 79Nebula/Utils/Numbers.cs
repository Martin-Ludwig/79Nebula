using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Utils
{
    public static class Numbers
    {
        
        /// <summary>
        /// Rounds midpoint away from zero with zero fractional digits. Converts Double to int.
        /// </summary>
        public static int RoundToInt(double n)
        {
            return Convert.ToInt32(Math.Round(n, 0, MidpointRounding.AwayFromZero));
        }

    }
}
