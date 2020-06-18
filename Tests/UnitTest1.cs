using Nebula._79Nebula.Models;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            Player player = new Player("name", 1, 1, 1, null);
            Player player1 = new Player("Player 1", 1, 1, 1, null);


            Assert.Pass();
        }
    }
}