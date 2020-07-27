using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Enums
{
    public enum AutoBattleState
    {
        Error = 0,
        // ErrorPlayerLost = 1,
        // ErrorPlayerWon = 2,

        IsPreparing = 10,

        IsRunning = 100,
        Lost = 101,
        Won = 102,
        Draw = 103
    }

}
