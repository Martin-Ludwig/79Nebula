using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {

        }

        [Test]
        public void Test2()
        {
            Player player = new Player("name", 1, 1, 1, new List<String> { } );
            Player player1 = new Player("Player 1", 1, 1, 1, new List<String> { });

            Module m;
            try
            {
                m = Modules.Get("0");
            }
            catch (Exception)
            {
                Assert.Fail();
                throw;
            }

            m.Activate(player, player1);

            Assert.AreEqual(2, player.Strength);
            Assert.AreEqual(0, player1.Strength);

            Console.WriteLine(player);
            Console.WriteLine(player1);

            Assert.Pass();
        }

        [Test]
        public void ApplyEffectTest()
        {
            Player player = new Player("name", 1, 1, 1, new List<String> { });

            Console.WriteLine(player);

            Effect e = new InitiatorBonus();
            player.ApplyEffect(new InitiatorBonus());

            Console.WriteLine(player);

            Assert.AreEqual(2, player.Strength);
            Assert.AreEqual(2, player.Agility);
            Assert.AreEqual(2, player.Intelligence);
            
            Assert.Pass();
        }


    }
}