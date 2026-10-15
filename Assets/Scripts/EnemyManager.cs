using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float timeBetweenSpawns = 1;

    private float lastSpawnTimestamp = 0;

    [SerializeField]
    private float minSpawnDistance;

    [SerializeField]
    private Vector2 spawnCenter;

    [SerializeField]
    private Vector2 spawnDimensions;

    private void OnValidate() {
        if(minSpawnDistance * 2 > spawnDimensions.x) {
            minSpawnDistance = spawnDimensions.x / 2;
        }   

        if (minSpawnDistance * 2 > spawnDimensions.y) {
            minSpawnDistance = spawnDimensions.y / 2;
        }
    }

    private void Update() {
        if(Time.time - lastSpawnTimestamp < timeBetweenSpawns) {
            return;
        }

        lastSpawnTimestamp = Time.time;

        Vector2 spawnPos;
        do {
            spawnPos = spawnCenter + new Vector2(Random.Range(-spawnDimensions.x / 2, spawnDimensions.x / 2), Random.Range(-spawnDimensions.y / 2, spawnDimensions.y / 2));
        }
        while (spawnPos.IsWithinDistance(playerCommonData.PlayerPosition, minSpawnDistance));

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(spawnCenter, spawnDimensions);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCommonData.PlayerPosition, minSpawnDistance);
    }
}
