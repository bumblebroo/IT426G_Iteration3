using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform corner1, corner2;

    [SerializeField]
    private float timeBetweenSpawns = 1;

    private float lastSpawnTimestamp = 0;

    [SerializeField]
    private float minSpawnDistance;

    private float SpawnWidth => Mathf.Abs(corner1.position.x - corner2.position.x);
    private float SpawnHeight => Mathf.Abs(corner1.position.y - corner2.position.y);
    private Vector3 SpawnCenter => Vector3.Lerp(corner1.position, corner2.position, 0.5f);

    private void Update() {
        if(Time.time - lastSpawnTimestamp < timeBetweenSpawns) {
            return;
        }
        lastSpawnTimestamp = Time.time;
        Vector2 centerOffset;
        do {
            centerOffset = new Vector2(Random.Range(-SpawnWidth / 2, SpawnWidth / 2), Random.Range(-SpawnHeight / 2, SpawnHeight / 2));
        }
        while (!CanSpawnEnemyHere((Vector2)SpawnCenter + centerOffset, playerCommonData.PlayerPosition, minSpawnDistance));

        Instantiate(enemyPrefab, (Vector2)SpawnCenter + centerOffset, Quaternion.identity);
    }

    private static bool CanSpawnEnemyHere(Vector2 enemyPos, Vector2 playerPos, float minDistance) {
        if(Vector2.SqrMagnitude(playerPos + enemyPos) > Mathf.Pow(minDistance, 2)) {
            return true;
        }
        return false;
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(SpawnCenter, new Vector3(SpawnWidth, SpawnHeight));
    }
}
