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
        public void StrengthModificationTest()
        {
            int str = player.Strength;
            int att = player.Attack;
            int def = player.Defense;
            int hel = player.Healing;
            int vit = player.Vitality;
            int hp = player.Health;

            player.StrengthModifier += 1;

            Assert.AreEqual(str + 1, player.Strength);
            Assert.AreEqual(att + 1, player.Attack);
            Assert.AreEqual(def + 1, player.Defense);
            Assert.AreEqual(hel + 1, player.Healing);
            Assert.AreEqual(vit + 2, player.Vitality);
            Assert.AreEqual(hp + (2 * Player.VitalityMultiplier), player.Health);
        }

        [Test]
        public void AgilityModificationTest()
        {
            int agi = player.Agility;
            int def = player.Defense;
            int init = player.Initiative;
            int vit = player.Vitality;
            int hp = player.Health;

            player.AgilityModifier += 1;

            Assert.AreEqual(agi + 1, player.Agility);
            Assert.AreEqual(def + 2, player.Defense);
            Assert.AreEqual(init + 2, player.Initiative);
            Assert.AreEqual(vit + 1, player.Vitality);
            Assert.AreEqual(hp + (Player.VitalityMultiplier), player.Health);
        }

        [Test]
        public void IntelligenceModificationTest()
        {
            int intl = player.Intelligence;
            int att = player.Attack;
            int hel = player.Healing;
            int init = player.Initiative;

            player.IntelligenceModifier += 1;

            Assert.AreEqual(intl +1, player.Intelligence);
            Assert.AreEqual(att + 2, player.Attack);
            Assert.AreEqual(hel + 2, player.Healing);
            Assert.AreEqual(init + 1, player.Initiative);
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

            player.TakeDamage(1, player.Attack, true);
            Assert.AreEqual(hp-1, player.Health);

            player.TakeDamage(player.Defense + 1, player.Attack);
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
