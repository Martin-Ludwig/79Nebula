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
            p1 = new Player("Test Player", WeaponType.Untyped, new List<string>() {
                "Default1", "Default2", "Default3", "Default4", "Default5"
            });


            enemies = Utils.Utils.DebugEnemies();

        }


    }
}