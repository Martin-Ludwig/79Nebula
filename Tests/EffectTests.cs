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

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ApplyEffectTest()
        {
            Player player = new Player("1", 1, 1, 1, new List<string> { });
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));

            Assert.Pass();
        }

        [Test]
        public void ApplyEffectTest2()
        {
            Player player = new Player("1", 1, 1, 1, new List<string> { });

            Assert.IsTrue(player.ApplyEffect(new InitiatorBonus()));

            Assert.Pass();
        }

        [Test]
        public void HasEffectTest1()
        {
            Player player = new Player("1", 1, 1, 1, new List<string> { });
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.AreEqual(1, player.HasEffect(effect));

            Assert.Pass();
        }

        [Test]
        public void HasEffectTest2()
        {
            Player player = new Player("1", 1, 1, 1, new List<string> { });
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.AreEqual(1, player.HasEffect(new InitiatorBonus()));

            Assert.Pass();
        }

        [Test]
        public void RemoveEffectByEffectTest1()
        {
            Player player = new Player("1", 1, 1, 1, new List<string> { });
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.IsTrue(player.RemoveEffect(new InitiatorBonus()));

            Assert.AreEqual(0, player.HasEffect(effect));
            Assert.AreEqual(0, player.HasEffect(new InitiatorBonus()));

            Assert.Pass();
        }

        [Test]
        public void RemoveEffectByEffectTest2()
        {
            Player player = new Player("1", 1, 1, 1, new List<string> { });
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.IsTrue(player.RemoveEffect(effect));

            Assert.AreEqual(0, player.HasEffect(effect));
            Assert.AreEqual(0, player.HasEffect(new InitiatorBonus()));

            Assert.Pass();
        }

        [Test]
        public void RemoveEffectByHashcodeTest()
        {
            Player player = new Player("1", 1, 1, 1, new List<string> { });
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.IsTrue(player.RemoveEffect(effect.GetHashCode()));

            Assert.AreEqual(0, player.HasEffect(effect));
            Assert.AreEqual(0, player.HasEffect(new InitiatorBonus()));

            Assert.Pass();

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
