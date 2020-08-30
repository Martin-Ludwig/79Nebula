using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Interfaces;
using Nebula._79Nebula.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    class EffectTests
    {

        class DummyEffect : Effect, IDuration
        {
            public override string Name => "Test Effect";

            public override string Description => $"Testing.";

            public override List<EffectType> EffectTypes => new List<EffectType>(){
                EffectType.Duration
            };

            public int Duration { get; set; }

            public DummyEffect(int duration)
            {
                Duration = duration;
            }
            public override void OnRoundEnd(Player player)
            {
                this.DurationExpire(this);
            }

            public override bool Equals(object obj)
            {
                if (!base.Equals(obj))
                {
                    return false;
                }

                var other = obj as DummyEffect;

                if (!this.Duration.Equals(other.Duration))
                {
                    return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                return this.Duration * 17 + base.GetHashCode();
            }

        }

        Player player;

        [SetUp]
        public void Setup()
        {
            player = new Player("Test Player", WeaponType.Untyped, new List<string>() {
                "Default1", "Default2", "Default3", "Default4", "Default5"
            });
        }

        [Test]
        public void ApplyEffect()
        {
            Effect effect = new InitiatorBonus();

            Assert.IsTrue(player.ApplyEffect(effect));
            Assert.AreEqual(1, player.HasEffect(effect));

            Assert.IsTrue(player.ApplyEffect(new InitiatorBonus()));
            Assert.AreEqual(2, player.HasEffect(new InitiatorBonus()));
        }

        [Test]
        public void RemoveEffect()
        {

            Assert.IsTrue(player.ApplyEffect(new InitiatorBonus()));
            Assert.IsTrue(player.ApplyEffect(new InitiatorBonus()));

            Assert.AreEqual(2, player.HasEffect(new InitiatorBonus()));

            Assert.IsTrue(player.RemoveEffect(new InitiatorBonus()));

            Effect e = new InitiatorBonus();
            Assert.AreEqual(1, player.HasEffect(e));
            Assert.IsTrue(player.RemoveEffect(e));
            Assert.AreEqual(0, player.HasEffect(new InitiatorBonus()));

            Assert.IsFalse(player.RemoveEffect(e));

        }

        [Test]
        public void Duration()
        {

            player.ApplyEffect(new DummyEffect(3));
            player.ApplyEffect(new DummyEffect(2));
            player.ApplyEffect(new DummyEffect(1));
            Console.WriteLine(player.Effects.CountActive);

            Assert.AreEqual(3, player.HasEffect(new DummyEffect(0)));

            player.Effects.OnRoundEnd(player);

            Console.WriteLine(player.Effects.CountActive);
            Assert.AreEqual(2, player.HasEffect(new DummyEffect(0)));

            player.Effects.OnRoundEnd(player);
            Console.WriteLine(player.Effects.CountActive);
            Assert.AreEqual(1, player.HasEffect(new DummyEffect(0)));

            player.Effects.OnRoundEnd(player);
            Console.WriteLine(player.Effects.CountActive);
            Assert.AreEqual(0, player.HasEffect(new DummyEffect(0)));

        }

        [TestCase(1, 2, false)]
        [TestCase(3, 3, true)]
        public void EqualEffectWithDuration(int x, int y, bool expected)
        {
            Assert.AreEqual(expected, new DummyEffect(x).Equals(new DummyEffect(y)));
        }

        [Test]
        public void ApplyEffectWithStacks()
        {
            player.ApplyEffect(new Regeneration(5));
            
            Effect e = player.Effects.FindEffect(new Regeneration());
            Assert.AreEqual(5, e.StackSize);
        }

        [Test]
        public void RemoveEffectStacksTest()
        {
            player.ApplyEffect(new Regeneration(5));

            int removedStacks = player.RemoveEffectStacks(new Regeneration(), 3);
            Assert.AreEqual(3, removedStacks);

            Effect e = player.Effects.FindEffect(new Regeneration());
            Assert.AreEqual(2, e.StackSize);
        }

        [Test]
        public void DeactivateEffectByZeroStacks()
        {
            player.ApplyEffect(new Regeneration(5));

            int removedStacks = player.RemoveEffectStacks(new Regeneration(), 7);
            Assert.AreEqual(5, removedStacks);

            Effect e = player.Effects.FindEffect(new Regeneration());
            Assert.IsNull(e);
        }

        [Test]
        public void ApplyStacks()
        {
            player.ApplyEffect(new Regeneration(5));
            player.ApplyEffect(new Regeneration(5));

            Effect e = player.Effects.FindEffect(new Regeneration());
            Assert.AreEqual(10, e.StackSize);
        }

        [Test]
        public void EffectStacksBigTest()
        {
            Effect e;

            player.ApplyEffect(new Regeneration(4));
            player.ApplyEffect(new Regeneration(4));
            player.ApplyEffect(new Regeneration(4));

            e = player.Effects.FindEffect(new Regeneration());
            Console.WriteLine(e.StackSize);

            int removedStacks = player.RemoveEffectStacks(new Regeneration(), 7);
            int removedStacks2 = player.RemoveEffectStacks(new Regeneration(), 3);

            Assert.AreEqual(7, removedStacks);
            Assert.AreEqual(3, removedStacks2);

            e = player.Effects.FindEffect(new Regeneration());
            Assert.AreEqual(2, e.StackSize);

            int removedStacks3 = player.RemoveEffectStacks(new Regeneration(), 99);
            player.ApplyEffect(new Regeneration(4));

            e = player.Effects.FindEffect(new Regeneration());
            Assert.AreEqual(4, e.StackSize);

            Assert.AreEqual(2, removedStacks3);
        }

    }
}
