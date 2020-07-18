using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class PlayerCrationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ZeroModulesTest()
        {
            Assert.Throws<PlayerCreationException>(
                delegate { 
                    new Player("Test Player", 1, 1, 1, 
                        new List<string>() { }); 
                });

            Assert.Pass();
        }

        [Test]
        public void FifeModulesTest()
        {
            Player test;
            test = new Player("Test Player", 1, 1, 1,
                new List<string>() { "Default1", "Default2", "Default3", "Default4", "Default5" });
            
            Assert.IsNotNull(test);

            Assert.Pass();
        }

        [Test]
        public void SixModulesTest()
        {

            Assert.Throws<PlayerCreationException>(
                delegate {
                    new Player("Test Player", 1, 1, 1,
                        new List<string>() { "Default1", "Default2", "Default3", "Default4", "Default5", "Default6" });
                });

            Assert.Pass();
        }

        [Test]
        public void PriorityViolationTest()
        {
            Assert.Throws<PlayerCreationException>(
                delegate {
                    new Player("Test Player", 1, 1, 1,
                        new List<string>() { "Default1", "Default2", "Default3", "Default6", "Default5" }); 
                });

            Assert.Pass();
        }

    }
}
