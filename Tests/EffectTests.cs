using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    class EffectTests
    {

        Player player;

        [SetUp]
        public void Setup()
        {
            player = new Player("Test Player", 1, 1, 1, new List<string>() {
                "Default1", "Default2", "Default3", "Default4", "Default5"
            });
        }

        [Test]
        public void ApplyEffectTest()
        {
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));

            Assert.Pass();
        }

        [Test]
        public void ApplyEffectTest2()
        {
            Assert.IsTrue(player.ApplyEffect(new InitiatorBonus()));
        }

        [Test]
        public void HasEffectTest1()
        {
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.AreEqual(1, player.HasEffect(effect));
        }

        [Test]
        public void HasEffectTest2()
        {
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.AreEqual(1, player.HasEffect(new InitiatorBonus()));
        }

        [Test]
        public void RemoveEffectByEffectTest1()
        {
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.IsTrue(player.RemoveEffect(new InitiatorBonus()));

            Assert.AreEqual(0, player.HasEffect(effect));
            Assert.AreEqual(0, player.HasEffect(new InitiatorBonus()));
        }

        [Test]
        public void RemoveEffectByEffectTest2()
        {
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.IsTrue(player.RemoveEffect(effect));

            Assert.AreEqual(0, player.HasEffect(effect));
            Assert.AreEqual(0, player.HasEffect(new InitiatorBonus()));
        }

        [Test]
        public void RemoveEffectByHashcodeTest()
        {
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.IsTrue(player.RemoveEffect(effect.GetHashCode()));

            Assert.AreEqual(0, player.HasEffect(effect));
            Assert.AreEqual(0, player.HasEffect(new InitiatorBonus()));
        }

        [Test]
        public void HashcodeTest()
        {
            // Fails at 10,000
            int n = 1000;

            List<Effect> effects = new List<Effect>();

            for (int i = 0; i < n; i++)
                effects.Add(new InitiatorBonus());

            for (int i = 0; i < n; i++)
                for (int j = i+1; j < n; j++)
                    if (i != j)
                        Assert.AreNotEqual(effects.ElementAt(i).GetHashCode(), effects.ElementAt(j).GetHashCode());


            Assert.Pass();
        }

    }
}
