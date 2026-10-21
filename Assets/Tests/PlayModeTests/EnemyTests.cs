using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.TestTools;

[TestFixture]
public class EnemyTests
{
    private readonly string sceneName = "EnemySpawnerTesting";
    private Scene scene;
    private EnemySpawner enemyManager;

    private PlayerCommonData testingPlayerCommonData;
    private const string playerCommonDataPath = "Assets/TestingAssets/TestingPlayerCommonData.asset";

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        enemyManager = Object.FindFirstObjectByType<EnemySpawner>();
        testingPlayerCommonData = (PlayerCommonData)AssetDatabase.LoadMainAssetAtPath(playerCommonDataPath);
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

    [UnityTest]
    public IEnumerator ChaseTest() {
        yield return new WaitForEndOfFrame();
        EnemyChaseBehaviour enemy = Object.FindFirstObjectByType<EnemyChaseBehaviour>();

        Assert.AreNotEqual(null, enemy);

        Vector2 startPos = enemy.transform.position;
        Vector2 targetPos = startPos + Vector2.right * 5;
        testingPlayerCommonData.SetPlayerPosition(targetPos);

        yield return new WaitForSeconds(0.2f);

        Assert.Less(Vector2.Distance(enemy.transform.position, targetPos), Vector2.Distance(startPos, targetPos));
    }
}
