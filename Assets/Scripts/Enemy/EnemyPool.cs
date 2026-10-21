using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class EnemyPool {
    private ObjectPool<EnemyBehaviour> objectPool;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int objectPoolDefaultCapacity = 10;

    [SerializeField]
    private int objectPoolMaxSize = 100;

    [Header("Spawn values")]

    [SerializeField]
    private float timeBetweenSpawns = 1;

    [SerializeField]
    private float decayPerSpawn = 0.05f;

    private float currentTimeBetweenSpawns;
    private float lastSpawnTimestamp = 0;

    public bool CanSpawn => (Time.time - lastSpawnTimestamp) > currentTimeBetweenSpawns;

    public void Start() {
        currentTimeBetweenSpawns = timeBetweenSpawns;
    }

    public EnemyPool() {
        currentTimeBetweenSpawns = timeBetweenSpawns;

        objectPool = new ObjectPool<EnemyBehaviour>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: objectPoolDefaultCapacity,
            maxSize: objectPoolMaxSize
        );
    }

    #region Poolfunctions
    private EnemyBehaviour CreateEnemy() {
        GameObject newEnemyGameObject = GameObject.Instantiate(prefab);
        EnemyBehaviour newEnemy = newEnemyGameObject.GetComponent<EnemyBehaviour>();

        newEnemy.GetComponent<EnemyHealth>().OnDeath.AddListener(() => ReleaseEnemy(newEnemy));

        return newEnemy;
    }

    private void OnGetEnemy(EnemyBehaviour enemy) {
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(EnemyBehaviour enemy) {
        //enemy.DeInitialize();
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(EnemyBehaviour enemy) {
        GameObject.Destroy(enemy.gameObject);
    }
    #endregion

    public EnemyBehaviour GetEnemy() {
        return objectPool.Get();
    }

    public void ReleaseEnemy(EnemyBehaviour enemy) {
        objectPool.Release(enemy);
    }

    public void UpdateLastSpawn() {
        lastSpawnTimestamp = Time.time;
        currentTimeBetweenSpawns -= Mathf.Lerp(0, currentTimeBetweenSpawns, decayPerSpawn);
    }
}
