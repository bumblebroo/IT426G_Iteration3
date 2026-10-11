using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

[TestFixture]
public class MovementTests
{
    [Test]
    [TestCase(5, 5, 1, 1)]
    public void Construction(float speed, float acceleration, float xDir, float yDir) {
        Movement movement = new Movement(speed, acceleration);
        Vector2 dir = new Vector2(xDir, yDir);

        float startDist = Vector2.Distance(movement.Velocity, Mathf.Abs(speed) * dir);

        movement.ChangeVelocity(dir, Time.deltaTime);

        float afterDist = Vector2.Distance(movement.Velocity, Mathf.Abs(speed) * dir);

        Assert.Less(afterDist, startDist);
    }

    private const float ERROR_MARGIN = 0f;

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
}
