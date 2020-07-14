using Nebula._79Nebula.Enums;
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
        public readonly string Name;
        public readonly string Description;
        public readonly List<EffectType> EffectTypes;

        public Effect(string name, string desc, List<EffectType> effectTypes)
        {
            Name = name;
            Description = desc;
            EffectTypes = effectTypes;
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
