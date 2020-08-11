using Nebula._79Nebula.DataAccess;
using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Tests
{
    public class BattleInstanceTest
    {

        AutoBattle battle;
        Player p1;

        List<Enemy> enemies;

        [SetUp]
        public void Setup()
        {
            p1 = new Player("Test Player", 1, 1, 1, new List<string>() {
                "Default1", "Default2", "Default3", "Default4", "Default5"
            });

            string _file = System.IO.Path.Combine("../../../../79Nebula", @"Data\Enemies.json");
            string json = System.IO.File.ReadAllText(_file);
            Enemy_DataAccess eda = Enemy_DataAccess.FromJson(json);
            
            enemies = eda.Enemies;

        }


    }
}