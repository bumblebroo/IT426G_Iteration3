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
    }


    private static IEnumerable VelocityChangesTowardsDirectionTestCases {
        get {
            yield return new TestCaseData(3, 3, 1, 1).Returns(null);
            yield return new TestCaseData(4, 12, -1, -1).Returns(null);
            yield return new TestCaseData(2, 2, -1 , 1).Returns(null);
        }
    }

    [UnityTest]
    [TestCaseSource(nameof(VelocityChangesTowardsDirectionTestCases))]
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
    [TestCase(5, 5, 1,1)]
    [TestCase(-4, 2, -1, 1)]
    [TestCase(3, -6, 1, -1)]
    [TestCase(-1, -1, -1, -1)]
    public void VelocitySameForSameFrame(float speed, float acceleration, float xDir, float yDir) {
        Movement movement = new Movement(speed, acceleration);
        Vector2 dir = new Vector2(xDir, yDir).normalized;

        movement.ChangeVelocity(dir, Time.deltaTime);

        Vector2 velocity = movement.Velocity;

        movement.ChangeVelocity(dir, Time.deltaTime);

        Assert.AreEqual(velocity, movement.Velocity);
    }
}
