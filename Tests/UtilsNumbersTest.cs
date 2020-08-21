using Microsoft.VisualBasic.CompilerServices;
using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Exceptions;
using Nebula._79Nebula.Models;
using Nebula._79Nebula.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace Tests
{
    public class UtilsNumbersTest
    {


        [SetUp]
        public void Setup()
        {

        }


        [TestCase(0, 0.0)]
        [TestCase(1, 1)]
        [TestCase(1, 1.1)]
        [TestCase(3, 3.49999)]
        [TestCase(3, 2.5)]
        [TestCase(4, 3.9)]
        [TestCase(-1, -1)]
        [TestCase(-1, -1.1)]
        [TestCase(-3, -2.5)]
        [TestCase(-4, -3.9)]
        public void RoundToInt(int expected, double value)
        {

            int r = Numbers.RoundToInt(value);
            Console.WriteLine($"{value} -> {r}");

            Assert.AreEqual(expected, r);


        }


    }
}