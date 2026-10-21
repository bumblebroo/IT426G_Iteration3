using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class EnemyManagerTests
{
    private readonly string sceneName = "EnemyManagerTesting";
    private Scene scene;
    private EnemySpawner enemyManager;

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        enemyManager = Object.FindFirstObjectByType<EnemySpawner>();
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown() {
        yield return SceneManager.UnloadSceneAsync(scene);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CreatesEnemies() {
        yield return new WaitForEndOfFrame();
        Assert.AreNotEqual(null, Object.FindFirstObjectByType<EnemyBehaviour>());
    }

    [UnityTest]
    public IEnumerator HidesEnemyOnEnemyDeath() {
        yield return new WaitForEndOfFrame();
        EnemyHealth enemyHealth = Object.FindFirstObjectByType<EnemyHealth>();
        enemyHealth.TakeDamage(int.MaxValue);

        Assert.AreNotEqual(null, enemyHealth.gameObject, "Enemies are nedlessly destroyed");
        Assert.AreEqual(false, enemyHealth.gameObject.activeSelf, "Enemies aren't being hidden");
    }
}
