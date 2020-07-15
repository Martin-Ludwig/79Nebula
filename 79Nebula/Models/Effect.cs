﻿using Nebula._79Nebula.Enums;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public abstract class Effect
    {
        public virtual string Name => "Example Effect";
        public virtual string Description => "No description given.";
        public virtual List<EffectType> EffectTypes => new List<EffectType>(){
            EffectType.untyped
        };

        public Effect()
        {
        }

        /// <summary>
        /// Triggers when this effect is applied to a player.
        /// </summary>
        public virtual void OnApply(Player player) { }

        /// <summary>
        /// Triggers when this effect is removed from a player.
        /// </summary>
        public virtual void OnRemove(Player player) { }

        public override string ToString()
        {
            return $"{Name}, Type: {EffectTypes}\n\t{Description}";
        }


    }
}