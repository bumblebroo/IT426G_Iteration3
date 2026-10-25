using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class VectorToolsTests
{
    [Test]
    [TestCase(1, 2, 4, 5, 2)]
    [TestCase(0, 0, 1, 1, 1.4f)]
    [TestCase(0, 0, 20, 20, 40f)]
    [TestCase(-100, -100, 100, 100, 500)]
    public void SeparationFuncIsAccurate(float a, float b, float x, float y, float force) {
        Vector2 origin = new Vector2(a, b);
        Vector2[] neighbours = { new Vector2(x, y)};

        Vector2 separationForce = origin.SeparationForce(neighbours, force);

        Assert.LessOrEqual(Vector2.Dot(neighbours[0] - origin, separationForce), 0);
    }
}