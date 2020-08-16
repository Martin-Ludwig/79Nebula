using System;
using System.Collections.Generic;
using Nebula._79Nebula.Models;
using Nebula._79Nebula.Utils;
using Newtonsoft.Json;

namespace Nebula._79Nebula.DataAccess
{

    public class Enemy_DataAccess
    {
        private const string _file = "/Data/Enemies.json";
        public static List<Enemy> Enemies { get; set; } = null;


        public static List<Enemy> GetEnemies()
        {
            if (Enemies == null)
            {
                string json = FileHandler.ReadFile(_file);
                EnemyList enemies = JsonHandler.Deserialize<EnemyList>(json);

                return Enemies = enemies.Enemies;
            }
            else
            {
                return Enemies;
            }
        }

        public class EnemyList
        {
            [JsonProperty("Enemies", Required = Required.Always)]
            public List<Enemy> Enemies { get; set; }
        }
    }
}
