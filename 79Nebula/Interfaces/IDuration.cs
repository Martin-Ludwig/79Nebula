using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Interfaces
{
    public interface IDuration
    {
        int Duration { get; set; }
        int DurationPassed { get; set; }
        
        bool IsExpired()
        {
            return DurationPassed >= Duration;
        }

    }
}
