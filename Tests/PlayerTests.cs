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

        [SetUp]
        public void Setup()
        {
            player = new Player("Test Player", 1, 1, 1, new List<string>() { });
        }


    }
}
