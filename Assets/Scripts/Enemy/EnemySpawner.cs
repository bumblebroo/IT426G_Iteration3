using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private EnemyPool[] enemyPools;

    [Space]

    public UnityEvent<Vector3> OnEnemySpawn;

    [Space]

    [SerializeField]
    [Min(0)]
    private float minSpawnDistance;

    [SerializeField]
    private Vector2 spawnCenter;

    [SerializeField]
    private Vector2 spawnDimensions;

    public float MinSpawnDistance => minSpawnDistance;

    private void OnValidate() {
        if(minSpawnDistance * 2 > spawnDimensions.x) {
            minSpawnDistance = spawnDimensions.x / 2;
        }   

        if (minSpawnDistance * 2 > spawnDimensions.y) {
            minSpawnDistance = spawnDimensions.y / 2;
        }
    }

    private void Start() {
        for (int i = 0; i < enemyPools.Length; i++) {
            enemyPools[i].Start();
        }
    }

    private void Update() {
        for (int i = 0; i < enemyPools.Length; i++) {
            SpawnEnemy(enemyPools[i]);
        }
    }

    private void SpawnEnemy(EnemyPool enemyPool) {
        if (!enemyPool.CanSpawn) {
            return;
        }

        enemyPool.UpdateLastSpawn();

        EnemyBehaviour enemy = enemyPool.GetEnemy();
        enemy.Initalize(playerCommonData);
        enemy.transform.position = GetSpawnPos();

        OnEnemySpawn?.Invoke(enemy.transform.position);
    }

    // Used for tests
    public void ForceSpawn() {
        EnemyBehaviour enemy = enemyPools[0].GetEnemy();
        enemy.transform.position = GetSpawnPos();
        OnEnemySpawn?.Invoke(enemy.transform.position);
    }

    private Vector2 GetSpawnPos() {
        Vector2 spawnPos = spawnCenter + new Vector2(Random.Range(-spawnDimensions.x / 2, spawnDimensions.x / 2), Random.Range(-spawnDimensions.y / 2, spawnDimensions.y / 2));

        if(Vector2.Distance(spawnPos, playerCommonData.PlayerPosition) < minSpawnDistance) {
            return GetSpawnPos();
        }

        return spawnPos;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(spawnCenter, spawnDimensions);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCommonData.PlayerPosition, minSpawnDistance);
    }
}