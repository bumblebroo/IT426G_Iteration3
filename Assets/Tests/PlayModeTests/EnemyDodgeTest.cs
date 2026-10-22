using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class EnemyDodgeTest
{
    private readonly string sceneName = "EnemyDodgeTest";
    private Scene scene;
    private EnemyDodgeBehaviour enemy;
    private GameObject mockProjectile;

    private PlayerCommonData testingPlayerCommonData;
    private const string playerCommonDataPath = "Assets/TestingAssets/TestingPlayerCommonData.asset";
    private const int projectileLayer = 7;

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        testingPlayerCommonData = (PlayerCommonData)AssetDatabase.LoadMainAssetAtPath(playerCommonDataPath);

        enemy = Object.FindFirstObjectByType<EnemyDodgeBehaviour>();
        enemy.Initalize(testingPlayerCommonData);
        Assert.IsNotNull(enemy);

        mockProjectile = Object.FindFirstObjectByType<Projectile>().gameObject;
    }

    [UnityTearDown]
    public IEnumerator TearDown() {
        yield return SceneManager.UnloadSceneAsync(scene);
        yield return null;
    }

    private static IEnumerable EnemyBehaviourTests {
        get {
            yield return new TestCaseData(2, 4).Returns(null);
            yield return new TestCaseData(-5, 3).Returns(null);
            yield return new TestCaseData(-2, -10).Returns(null);
            yield return new TestCaseData(12, 200).Returns(null);
        }
    }

    [UnityTest]
    [TestCaseSource(nameof(EnemyBehaviourTests))]
    public IEnumerator DodgeTest(float offsetX, float offsetY) {
        Vector2 startPos = enemy.transform.position;

        Vector2 targetPos = startPos + Vector2.right * offsetX + Vector2.up * offsetY;
        mockProjectile.transform.position = targetPos;

        float timer = 0;
        while (timer < 1f) {
            // Effectively eliminates the movement towards the playerposition
            testingPlayerCommonData.SetPlayerPosition(enemy.transform.position);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Assert.GreaterOrEqual(Vector2.Distance(enemy.transform.position, targetPos), enemy.ProjectileDodgeRange);
    }
}
