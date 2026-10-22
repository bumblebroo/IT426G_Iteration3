using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class EnemyChaserTests
{
    private readonly string sceneName = "EnemyChaseTest";
    private Scene scene;
    private EnemyChaseBehaviour enemy;

    private PlayerCommonData testingPlayerCommonData;
    private const string playerCommonDataPath = "Assets/TestingAssets/TestingPlayerCommonData.asset";

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        testingPlayerCommonData = (PlayerCommonData)AssetDatabase.LoadMainAssetAtPath(playerCommonDataPath);

        enemy = Object.FindFirstObjectByType<EnemyChaseBehaviour>();
        enemy.Initalize(testingPlayerCommonData);
        Assert.IsNotNull(enemy);
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
    public IEnumerator ChaseTest(float offsetX, float offsetY) {
        Vector2 startPos = enemy.transform.position;
        Vector2 targetPos = startPos + Vector2.right * offsetX + Vector2.up * offsetY;
        testingPlayerCommonData.SetPlayerPosition(targetPos);

        yield return new WaitForSeconds(0.2f);

        Assert.Less(Vector2.Distance(enemy.transform.position, targetPos), Vector2.Distance(startPos, targetPos));
    }
}
