using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class PlayerMovementTests
{
    private readonly string sceneName = "PlayerMovementTest";
    private Scene scene;
    private PlayerMovementBehaviour player;

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        player = Object.FindFirstObjectByType<PlayerMovementBehaviour>();
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown() {
        yield return SceneManager.UnloadSceneAsync(scene);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MoveRight() {
        player.TakeInputRaw(Vector2.zero);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.AreEqual(Vector2.zero, player.CurrentVelocity, "Player isn't still");

        float x = player.transform.position.x;
        player.TakeInputRaw(Vector2.right);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.Greater(player.transform.position.x, x);
        Assert.AreEqual(Vector2.right * player.Speed, player.CurrentVelocity, "Player velocity isn't as expected");
    }

    [UnityTest]
    public IEnumerator MoveLeft() {
        player.TakeInputRaw(Vector2.zero);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.AreEqual(Vector2.zero, player.CurrentVelocity, "Player isn't still");

        float x = player.transform.position.x;
        player.TakeInputRaw(-Vector2.right);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.Less(player.transform.position.x, x);
        Assert.AreEqual(-Vector2.right * player.Speed, player.CurrentVelocity, "Player velocity isn't as expected");
    }

    [UnityTest]
    public IEnumerator MoveUp() {
        player.TakeInputRaw(Vector2.zero);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.AreEqual(Vector2.zero, player.CurrentVelocity, "Player isn't still");

        float y = player.transform.position.y;
        player.TakeInputRaw(Vector2.up);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.Greater(player.transform.position.y, y);
        Assert.AreEqual(Vector2.up * player.Speed, player.CurrentVelocity, "Player velocity isn't as expected");
    }

    [UnityTest]
    public IEnumerator MoveDown() {
        player.TakeInputRaw(Vector2.zero);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.AreEqual(Vector2.zero, player.CurrentVelocity, "Player isn't still");

        float y = player.transform.position.y;
        player.TakeInputRaw(-Vector2.up);
        yield return new WaitForSeconds(player.Acceleration);

        Assert.Less(player.transform.position.y, y);
        Assert.AreEqual(-Vector2.up * player.Speed, player.CurrentVelocity, "Player velocity isn't as expected");
    }
}