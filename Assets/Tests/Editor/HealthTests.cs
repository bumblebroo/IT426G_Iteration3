using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class HealthTests
{
    [Test]
    [TestCase(5)]
    [TestCase(120)]
    [TestCase(2)]
    [TestCase(-2350)]
    public void HealthSetMaxHealth(int maxHealth) {
        Health health = new Health(maxHealth);

        Assert.AreEqual(Mathf.Abs(maxHealth), health.CurrentHealth);
    }

    [Test]
    [TestCase(-2, 10)]
    [TestCase(2, 10)]
    [TestCase(-10, 5)]
    public void HealthIsWithinBounds(int healthDelta, int maxHealth) {
        Health health = new Health(maxHealth);

        health.ChangeHealth(healthDelta);

        Assert.GreaterOrEqual(health.CurrentHealth, 0);
        Assert.LessOrEqual(health.CurrentHealth, maxHealth);
    }

    [Test]
    [TestCase(-2, 10)]
    [TestCase(2, 10)]
    [TestCase(-10, 5)]
    public void HealthDecreaseInvokesOnDamage(int healthDelta, int maxHealth) {
        Health health = new Health(maxHealth);
        int eventsTriggered = 0;

        health.OnDamage.AddListener(() => eventsTriggered++);

        health.ChangeHealth(healthDelta);

        if(healthDelta < 0) {
            Assert.AreEqual(1, eventsTriggered);
        } else {
            Assert.AreEqual(0, eventsTriggered);
        }
    }

    [Test]
    [TestCase(-2, 10)]
    [TestCase(2, 10)]
    [TestCase(-10, 5)]
    public void HealthIncreaseInvokesOnHeal(int healthDelta, int maxHealth) {
        Health health = new Health(maxHealth);
        int eventsTriggered = 0;

        health.OnHeal.AddListener(() => eventsTriggered++);

        health.ChangeHealth(healthDelta);

        if (healthDelta > 0) {
            Assert.AreEqual(1, eventsTriggered);
        } else {
            Assert.AreEqual(0, eventsTriggered);
        }
    }

    [Test]
    [TestCase(-2, 10)]
    [TestCase(-6, 6)]
    [TestCase(-6, 10)]
    [TestCase(-10, 5)]
    public void CurrentHealthReaches0InvokesOnDeathOnce(int healthDelta, int maxHealth) {
        Health health = new Health(maxHealth);
        int eventsTriggered = 0;

        health.OnDeath.AddListener(() => eventsTriggered++);

        health.ChangeHealth(healthDelta);
        health.ChangeHealth(healthDelta);

        if ((maxHealth + healthDelta) <= 0 || (maxHealth + healthDelta * 2) <= 0) {
            Assert.AreEqual(1, eventsTriggered);
        } else {
            Assert.AreEqual(0, eventsTriggered);
        }
    }

    [Test]
    [TestCase(-2, 10)]
    [TestCase(-6, 6)]
    [TestCase(-6, 10)]
    [TestCase(-10, 5)]
    public void CurrentHealthResets(int healthDelta, int maxHealth) {
        Health health = new Health(maxHealth);
        health.ChangeHealth(healthDelta);

        Assert.AreNotEqual(maxHealth, health.CurrentHealth);

        health.Reset();

        Assert.AreEqual(maxHealth, health.CurrentHealth);

        Assert.AreEqual(health.CurrentHealth, maxHealth);
    }
}
