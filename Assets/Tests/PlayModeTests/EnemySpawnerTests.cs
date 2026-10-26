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

    private PlayerCommonData testingPlayerCommonData;
    private const string playerCommonDataPath = "Assets/TestingAssets/TestingPlayerCommonData.asset";

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);

        testingPlayerCommonData = (PlayerCommonData)AssetDatabase.LoadMainAssetAtPath(playerCommonDataPath);

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

    private const float minDistance = 30;
    private const int checkCount = 100;

    private static IEnumerable EnemyMinSpawnDistanceTestCases {
        get {
            yield return new TestCaseData(-2, 2).Returns(null);
            yield return new TestCaseData(2, 2).Returns(null);
            yield return new TestCaseData(2, 1).Returns(null);
            yield return new TestCaseData(2, -2).Returns(null);
        }
    }

    [UnityTest]
    [TestCaseSource(nameof(EnemyMinSpawnDistanceTestCases))]
    public IEnumerator EnemyMinSpawnDistance(float x, float y) {
        Vector2 pos = new Vector2(x, y);
        testingPlayerCommonData.SetPlayerPosition(pos);

        for (int i = 0; i < checkCount; i++) {
            enemySpawner.ForceSpawn();
        }

        EnemyBehaviour[] enemies = GameObject.FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None);

        int countInMinDistance = 0;
        for (int i = 0; i < checkCount; i++) {
            if (Vector2.Distance(pos, enemies[i].transform.position) < minDistance) {
                countInMinDistance++;
            }
        }

        Assert.AreEqual(0, countInMinDistance);
        yield return null;
    }
}
