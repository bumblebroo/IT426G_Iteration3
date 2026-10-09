using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class ScoreCounterTests
{
    [Test]
    [TestCase(10)]
    [TestCase(-10)]
    public void ScoreCounterDoesIncrease(int scoreDelta)
    {
        ScoreCounter scoreCounter = new ScoreCounter();
        scoreCounter.AddScore(scoreDelta);

        if(scoreDelta >= 0) {
            Assert.AreEqual(scoreDelta, scoreCounter.Score);
        }

        Assert.GreaterOrEqual(scoreCounter.Score, 0);
    }

    [Test]
    [TestCase(10)]
    [TestCase(-10)]
    public void ScoreCounterSetsPositiveStartScore(int startScore) {
        ScoreCounter scoreCounter = new ScoreCounter(startScore);

        if (startScore >= 0) {
            Assert.AreEqual(startScore, scoreCounter.Score);
        }

        Assert.GreaterOrEqual(scoreCounter.Score, 0);
    }


    [Test]
    [TestCase(10)]
    [TestCase(-10)]
    public void ScoreCounterSendsEventOnScoreIncrease(int scoreDelta) {
        ScoreCounter scoreCounter = new ScoreCounter();
        int eventsTriggered = 0;

        scoreCounter.OnScoreIncrease.AddListener(() => eventsTriggered++);

        scoreCounter.AddScore(scoreDelta);

        if (scoreDelta >= 0) {
            Assert.AreEqual(1, eventsTriggered);
        } else {
            Assert.AreEqual(0, eventsTriggered);
        }
    }
}
