using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class PlayerTests
    {
        // Todo

        Player player;
        int hp;

        [SetUp]
        public void Setup()
        {
            player = new Player("Test Player", 1, 1, 1, new List<string>() {
                "Default1", "Default2", "Default3", "Default4", "Default5"
            });

            hp = player.Health;
        }

        [Test]
        public void ModificationTests()
        {
            player.StrengthModifier += 1;
            Assert.AreEqual(2, player.Strength);

            player.AgilityModifier += 1;
            Assert.AreEqual(2, player.Agility);

            player.IntelligenceModifier += 1;
            Assert.AreEqual(2, player.Intelligence);
        }

        [Test]
        public void ModificationTests2()
        {
            int tmp;

            tmp = player.Attack;
            player.AttackModifier += 1;
            Assert.AreEqual(tmp + 1, player.Attack);

            tmp = player.Defense;
            player.DefenseModifier += 1;
            Assert.AreEqual(tmp + 1, player.Defense);

            tmp = player.Initiative;
            player.InitiativeModifier += 1;
            Assert.AreEqual(tmp + 1, player.Initiative);

            tmp = player.Healing;
            player.HealingModifier += 1;
            Assert.AreEqual(tmp + 1, player.Healing);

            tmp = player.Vitality;
            player.VitalityModifier += 1;
            Assert.AreEqual(tmp + 1, player.Vitality);
        }

        [Test]
        public void DamagedTest()
        {
            player.Damaged += 1;

            Assert.AreEqual(1, player.Damaged);
            Assert.AreEqual(hp - 1, player.Health);
        }

        [Test]
        public void HealedTest()
        {
            player.Healed += 1;

            Assert.AreEqual(1, player.Healed);
            Assert.AreEqual(hp + 1, player.Health);
        }

        [Test]
        public void TakeDamageTest()
        {
            player.TakeDamage(1);
            Assert.AreEqual(hp, player.Health);

            player.TakeDamage(1, true);
            Assert.AreEqual(hp-1, player.Health);

            player.TakeDamage(player.Defense + 1);
            Assert.AreEqual(hp-2, player.Health);
        }

        [Test]
        public void HealTest()
        {
            int healing = player.Healing;
            player.Heal(5);

            Assert.AreEqual(hp + healing + 5, player.Health);
        }

    }
}
