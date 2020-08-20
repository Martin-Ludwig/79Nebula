using System.Collections.Generic;

namespace Nebula._79Nebula.Models
{
    /// <summary>
    /// EffectList is an extended List<Effect> object.
    /// </summary>
    public class EffectList : List<Effect>
    {
        public int CountActive { get { return this.FindAll(o => o.IsActive).Count; } }

        /// <summary>
        /// Executes OnRoundEnd() for each effect.
        /// </summary>
        public void OnRoundEnd(Player player)
        {
            this.ForEach(o => {
                if (o.IsActive)
                {
                    o.OnRoundEnd(player);
                }
            });
        }

    }
}
