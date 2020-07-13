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

        public void OnApply(Player player) { }
        public void OnRemove(Player player) { }

        public override string ToString()
        {
            return $"{Name}, Type: {EffectTypes}\n\t{Description}";
        }

    }

    public enum EffectType
    {
        untyped,
        Buff,
        Debuff,
        Aura,
        Condition,
        Curse
    }
}
