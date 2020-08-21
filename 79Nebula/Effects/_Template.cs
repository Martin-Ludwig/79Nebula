using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Effects
{
    class _Template : Effect
    {
        public override string Name => base.Name;
        public override string Description => base.Description;
        public override List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.untyped
        };

    }
}
