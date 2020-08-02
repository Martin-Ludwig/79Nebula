
using Nebula._79Nebula.DataAccess;
using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Tests
{
    public class EnemyTests
    {

        private readonly string _file = Path.Combine("../../../../79Nebula", @"Data\Enemies.json");
        Enemy_DataAccess eda;

        [SetUp]
        public void Setup()
        {
            string json = System.IO.File.ReadAllText(_file);
            eda = Enemy_DataAccess.FromJson(json);
            Console.WriteLine(json);
        }

        [Test]
        public void DataExists()
        {
            Assert.IsNotNull(eda.Enemies);
        }

        [TestCase("Lizzie")]
        [TestCase("Mirza")]
        public void FindByName(string name)
        {
            Assert.IsNotNull(eda.Enemies.Find(o => o.Name == name));
        }

        [TestCase("Lizzie")]
        [TestCase("Mirza")]
        public void ToPlayer(string name)
        {
            Enemy e = eda.Enemies.Find(o => o.Name == name);
            Player p = e.ToPlayer();

            Assert.IsNotNull(p);

            Console.WriteLine(p);
        }

    }
}