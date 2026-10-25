using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

[TestFixture]
public class MovementTests
{
    [Test]
    [TestCase(5, 5)]
    [TestCase(-4, 2)]
    [TestCase(3, -6)]
    [TestCase(-1, -1)]
    public void MovementConstruction(float speed, float acceleration) {
        Movement movement = new Movement(speed, acceleration);

        Assert.Greater(movement.Speed, 0);
        Assert.Greater(movement.Acceleration, 0);
        Assert.AreEqual(Mathf.Abs(speed), movement.Speed);
        Assert.AreEqual(Mathf.Abs(acceleration), movement.Acceleration);
    }

    [Test]
    [TestCase(5, 5, 1, 1)]
    [TestCase(-4, 2, -1, -1)]
    [TestCase(3, -6, -1, 1)]
    [TestCase(-1, -1, 1, -1)]
    public void MovementReset(float speed, float acceleration, float xDir, float yDir) {
        Movement movement = new Movement(speed, acceleration);
        Vector2 dir = new Vector2(xDir, yDir).normalized;
        Vector2 targetVelocity = dir * speed;

        movement.ChangeVelocity(dir, Time.deltaTime);
        Assert.AreNotEqual(Vector2.zero, movement.Velocity);
        movement.Reset();
        Assert.AreEqual(Vector2.zero, movement.Velocity);
    }

    private static IEnumerable IEnumeratorSources {
        get {
            yield return new TestCaseData(3, 3, 1, 1).Returns(null);
            yield return new TestCaseData(4, 12, -1, -1).Returns(null);
            yield return new TestCaseData(2, 2, -1 , 1).Returns(null);
            yield return new TestCaseData(100, 0.1f, 1, -1).Returns(null);
        }
    }

    [UnityTest]
    [TestCaseSource(nameof(IEnumeratorSources))]
    public IEnumerator VelocityChangesTowardsDirection(float speed, float acceleration, float xDir, float yDir) {
        Movement movement = new Movement(speed, acceleration);
        Vector2 dir = new Vector2(xDir, yDir).normalized;
        Vector2 targetVelocity = dir * speed;

        float time = 0;
        while (time < acceleration) {
            movement.ChangeVelocity(dir, Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        Assert.AreEqual(targetVelocity, movement.Velocity);
    }

    [Test]
    [TestCaseSource(nameof(IEnumeratorSources))]
    public IEnumerator VelocityMagnitudeDoesNotExceedSpeed(float speed, float acceleration, float xDir, float yDir) {
        Movement movement = new Movement(speed, acceleration);
        Vector2 dir = new Vector2(xDir, yDir).normalized;
        Vector2 targetVelocity = dir * speed;

        float time = 0;
        while (time < acceleration * 2) {
            movement.ChangeVelocity(dir, Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        Assert.LessOrEqual(movement.Velocity.magnitude, targetVelocity.magnitude);
    }
}
