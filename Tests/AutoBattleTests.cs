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
    public class AutoBattleTests
    {

        AutoBattle battle;
        Player p1;
        Player p2;

        [SetUp]
        public void Setup()
        {
            battle = new AutoBattle();
            p1 = new Player("Test Player", WeaponType.Untyped, new List<string>() {
                "Default1", "Default2", "Default3", "Default4", "Default5"
            });

            p2 = new Player("Test Player", WeaponType.Untyped, new List<string>() {
                "Default1", "Default2", "Default3", "Default4", "Default5"
            });

        }


        [Test]
        public void Lost()
        {
            p2.StrengthModifier += 2;
            Assert.AreEqual(AutoBattleState.Lost, battle.Battle(p1, p2));
        }

        [Test]
        public void Won()
        {
            p1.StrengthModifier += 2;
            Assert.AreEqual(AutoBattleState.Won, battle.Battle(p1, p2));
        }

        [Test]
        public void Draw()
        {
            Assert.AreEqual(AutoBattleState.Draw, battle.Battle(p1, p2));
        }


    }
}