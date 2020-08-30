using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class Enemy
    {
        [JsonProperty("Name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("Weapon", Required = Required.Always)]
        public WeaponType weaponType{ get; set; }

        [JsonProperty("Modules", Required = Required.Always)]
        public List<string> Modules { get; set; }


        /// <summary>
        /// Returns a player object from the enemy values.
        /// </summary>
        public Player ToPlayer()
        {
            return new Player(Name, weaponType, Modules);
        }

        /// <summary>
        /// Returns a player base object from the enemy values.
        /// </summary>
        public PlayerBase ToPlayerBase()
        {
            return new PlayerBase(Name, weaponType, Modules);
        }

    }
}