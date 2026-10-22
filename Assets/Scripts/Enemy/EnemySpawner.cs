using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner instance;

    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private EnemyPool dodgePool = new EnemyPool();

    [SerializeField]
    private EnemyPool chaserPool = new EnemyPool();

    [SerializeField]
    private EnemyPool starPool = new EnemyPool();


    [SerializeField]
    [Min(0)]
    private float minSpawnDistance;

    [SerializeField]
    private Vector2 spawnCenter;

    [SerializeField]
    private Vector2 spawnDimensions;

    public float MinSpawnDistance => minSpawnDistance;
    public static EnemySpawner Instance => instance;

    private void OnValidate() {
        if(minSpawnDistance * 2 > spawnDimensions.x) {
            minSpawnDistance = spawnDimensions.x / 2;
        }   

        if (minSpawnDistance * 2 > spawnDimensions.y) {
            minSpawnDistance = spawnDimensions.y / 2;
        }
    }


    private void Awake() {
        if(instance != null) {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    private void Start() {
        dodgePool.Start();
        chaserPool.Start();
        starPool.Start();
    }

    private void Update() {
        SpawnEnemy(dodgePool);
        SpawnEnemy(chaserPool);
        SpawnEnemy(starPool);
    }

    private void SpawnEnemy(EnemyPool enemyPool) {
        if (!enemyPool.CanSpawn) {
            return;
        }

        enemyPool.UpdateLastSpawn();

        EnemyBehaviour enemy = enemyPool.GetEnemy();
        enemy.Initalize(playerCommonData);
        enemy.transform.position = GetSpawnPos();
    }

    private Vector2 GetSpawnPos() {
        Vector2 spawnPos = spawnCenter + new Vector2(Random.Range(-spawnDimensions.x / 2, spawnDimensions.x / 2), Random.Range(-spawnDimensions.y / 2, spawnDimensions.y / 2));

        if (spawnPos.IsWithinDistance(playerCommonData.PlayerPosition, minSpawnDistance)) {
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