using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.TestTools;

[TestFixture]
public class EnemySpawnerTests
{
    private readonly string sceneName = "EnemySpawnerTesting";
    private Scene scene;
    private EnemySpawner enemySpawner;

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        enemySpawner = Object.FindFirstObjectByType<EnemySpawner>();
    }

    [UnityTearDown]
    public IEnumerator TearDown() {
        yield return SceneManager.UnloadSceneAsync(scene);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CreatesEnemies() {
        yield return new WaitForEndOfFrame();
        Assert.IsNotNull(Object.FindFirstObjectByType<EnemyBehaviour>());
    }

    [UnityTest]
    public IEnumerator HidesEnemyOnEnemyDeath() {
        yield return new WaitForEndOfFrame();
        EnemyHealth enemyHealth = Object.FindFirstObjectByType<EnemyHealth>();
        enemyHealth.TakeDamage(int.MaxValue);

        Assert.IsNotNull(enemyHealth.gameObject, "Enemies are nedlessly destroyed");
        Assert.AreEqual(false, enemyHealth.gameObject.activeSelf, "Enemies aren't being hidden");
    }

    [UnityTest]
    public IEnumerator EnemyMinSpawnDistance() {
        int checkCount = 1000;

        for (int i = 0; i < checkCount; i++) {
            enemySpawner.ForceSpawn();
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector2.zero, 1f);

        Assert.AreEqual(0, colliders.Length);
        yield return null;
    }
}
