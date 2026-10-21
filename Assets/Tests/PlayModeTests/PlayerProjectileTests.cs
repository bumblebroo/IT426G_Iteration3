using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerProjectileTests
{
    private readonly string sceneName = "PlayerProjectileTests";
    private Scene scene;
    private EnemyHealth enemyHealth;
    private ProjectileManager projectileManager;

    [UnitySetUp]
    public IEnumerator Setup() {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        projectileManager = ProjectileManager.Instance;
        enemyHealth = Object.FindFirstObjectByType<EnemyHealth>();
        enemyHealth.Initalize();
        Assert.AreNotEqual(null, projectileManager);
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown() {
        yield return SceneManager.UnloadSceneAsync(scene);
        yield return null;
    }


    [UnityTest]
    public IEnumerator ProjectileHitsEnemy() {
        int hitEventsTriggered = 0;

        ProjectileData projectileData = ScriptableObject.CreateInstance<ProjectileData>();

        Projectile projectile = projectileManager.GetProjectile();
        projectile.Initialize(projectileData, Vector2.zero, Vector2.right * 10);

        enemyHealth.OnHit.AddListener(() => hitEventsTriggered++);

        yield return new WaitForSeconds(1);

        Assert.AreEqual(1, hitEventsTriggered);
    }

    [UnityTest]
    public IEnumerator ProjectileDoesntHitEnemy() {
        int hitEventsTriggered = 0;

        ProjectileData projectileData = ScriptableObject.CreateInstance<ProjectileData>();

        Projectile projectile = projectileManager.GetProjectile();
        projectile.Initialize(projectileData, Vector2.zero, Vector2.right * -10);

        enemyHealth.OnHit.AddListener(() => hitEventsTriggered++);

        yield return new WaitForSeconds(1);

        Assert.AreEqual(0, hitEventsTriggered);
    }

    [UnityTest]
    public IEnumerator ProjectilesKillEnemyOnce() {
        int hitEventsTriggered = 0;

        ProjectileData projectileData = ScriptableObject.CreateInstance<ProjectileData>();

        int projectileAmount = 5;

        for (int i = 0; i < projectileAmount; i++) {
            Projectile projectile = projectileManager.GetProjectile();
            projectile.Initialize(projectileData, Vector2.zero, Vector2.right * 10);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector2.zero, 1);
        Assert.AreEqual(projectileAmount, colliders.Length);

        enemyHealth.OnDeath.AddListener(() => hitEventsTriggered++);

        yield return new WaitForSeconds(1);

        Assert.AreEqual(0, enemyHealth.CurrentHealth);
        Assert.AreEqual(1, hitEventsTriggered);
    }
}
