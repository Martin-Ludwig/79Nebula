using Nebula._79Nebula.Models;
using Nebula._79Nebula.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Nebula._79Nebula.DataAccess.Enemy_DataAccess;

namespace Tests.Utils
{
    static class Utils
    {

        /// <summary>
        /// Returns enemies
        /// </summary>
        public static List<Enemy> DebugEnemies()
        {
            string _file = Path.Combine("../../../../79Nebula", @"Data\Enemies.json");
            string json = System.IO.File.ReadAllText(_file);

            EnemyList e = JsonHandler.Deserialize<EnemyList>(json);

            return e.Enemies;
        }
    }
}
