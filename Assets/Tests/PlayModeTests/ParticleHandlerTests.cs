using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ParticleHandlerTests
{
    private readonly string sceneName = "ParticleHandlerTest";
    private Scene scene;
    private ParticleHandler particleHandler;

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);

        particleHandler = Object.FindFirstObjectByType<ParticleHandler>();
        Assert.IsNotNull(particleHandler);
    }

    [UnityTearDown]
    public IEnumerator TearDown() {
        yield return SceneManager.UnloadSceneAsync(scene);
        yield return null;
    }
    private static IEnumerable TestCases {
        get {
            yield return new TestCaseData(2, 4).Returns(null);
            yield return new TestCaseData(-5, 3).Returns(null);
            yield return new TestCaseData(-2, -10).Returns(null);
            yield return new TestCaseData(12, 200).Returns(null);
        }
    }

    [UnityTest]
    [TestCaseSource(nameof(TestCases))]
    public IEnumerator PlacesAndPlaysParticle(float x, float y) {
        Vector3 pos = new Vector2(x, y);

        particleHandler.SummonParticle(pos);

        ParticleSystem particleSystem = Object.FindFirstObjectByType<ParticleSystem>();
        Assert.IsNotNull(particleSystem);
        Assert.IsTrue(particleSystem.isPlaying);
        Assert.AreEqual(pos, particleSystem.transform.position);
        yield return null;
    }
}
