using System;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;

    private ObjectPool<Projectile> objectPool;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private int objectPoolDefaultCapacity = 10;

    [SerializeField]
    private int objectPoolMaxSize = 100;

    public static ProjectileManager Instance => instance;
    private void Awake() {
        if(instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        objectPool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: OnGetProjectile,
            actionOnRelease: OnReleaseProjectile,
            actionOnDestroy: OnDestroyProjectile,
            collectionCheck: true,
            defaultCapacity: objectPoolDefaultCapacity,
            maxSize: objectPoolMaxSize
        );
    }

    private Projectile CreateProjectile() {
        return Instantiate(projectilePrefab).GetComponent<Projectile>();
    }

    private void OnGetProjectile(Projectile projectile) {
        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseProjectile(Projectile projectile) {
        projectile.DeInitialize();
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile) {
        Destroy(projectile.gameObject);
    }

    public Projectile GetProjectile() {
        return objectPool.Get();
    }

    public void ReleaseProjectile(Projectile projectile) {
        objectPool.Release(projectile);
    }
}
