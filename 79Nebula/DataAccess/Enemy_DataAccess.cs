using System.Collections.Generic;

using Nebula._79Nebula.Models;
using Newtonsoft.Json;

namespace Nebula._79Nebula.DataAccess
{

    public class Enemy_DataAccess
    {
        [JsonProperty("Enemies", Required = Required.Always)]
        public List<Enemy> Enemies { get; set; }
        
        /// <summary>
        /// Converts a json string to a list of enemies.
        /// </summary>
        public static Enemy_DataAccess FromJson(string json) => JsonConvert.DeserializeObject<Enemy_DataAccess>(json);

    }
}
