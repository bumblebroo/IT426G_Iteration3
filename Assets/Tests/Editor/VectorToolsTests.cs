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
    public void DistanceCheckIsAccurate(float a, float b, float x, float y, float distance) {
        Vector2 ab = new Vector2(a, b);
        Vector2 xy = new Vector2(x, y);

        Assert.AreEqual(Vector2.Distance(ab, xy) < distance, ab.IsWithinDistance(xy, distance));
    }
}