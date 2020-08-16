
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tests.Utils;

namespace Tests
{
    public class EnemyTests
    {

        List<Enemy> Enemies;

        [SetUp]
        public void Setup()
        {
            Enemies = Utils.Utils.DebugEnemies();
        }

        [Test]
        public void DataExists()
        {
            Assert.IsNotNull(Enemies);
        }

        [TestCase("Lizzie")]
        [TestCase("Mirza")]
        public void FindByName(string name)
        {
            Assert.IsNotNull(Enemies.Find(o => o.Name == name));
        }

        [TestCase("Lizzie")]
        [TestCase("Mirza")]
        public void ToPlayer(string name)
        {
            Enemy e = Enemies.Find(o => o.Name == name);
            Player p = e.ToPlayer();

            Assert.IsNotNull(p);

            Console.WriteLine(p);
        }

    }
}