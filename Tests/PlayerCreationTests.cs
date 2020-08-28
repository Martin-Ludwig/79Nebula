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
        public void ZeroModules()
        {
            Assert.Throws<PlayerCreationException>(
                delegate { 
                    new Player("Test Player", 1, 1, 1, 
                        new List<string>() { }); 
                });

            Assert.Pass();
        }

        [Test]
        public void FifeModules()
        {
            Player test = new Player("Test Player", 1, 1, 1,
                new List<string>() { "Default1", "Default2", "Default3", "Default4", "Default5" });
            
            Assert.IsNotNull(test);

            Assert.Pass();
        }

        [Test]
        public void SixModules()
        {

            Assert.Throws<PlayerCreationException>(
                delegate {
                    new Player("Test Player", 1, 1, 1,
                        new List<string>() { "Default1", "Default2", "Default3", "Default4", "Default5", "Default6" });
                });

            Assert.Pass();
        }

        [Test]
        public void PriorityViolation()
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
