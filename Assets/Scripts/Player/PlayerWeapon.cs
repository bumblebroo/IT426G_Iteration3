using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private SFXHandler sfxHandler;

    [Space]

    [SerializeField]
    private float rps;

    [SerializeField]
    private ProjectileData projectileData;

    [SerializeField]
    private Transform[] firePoints;

    private float lastShotTimestamp;

    public UnityEvent OnShoot;

    public void Shoot() {
        if (Time.time - lastShotTimestamp < 1 / rps) {
            return;
        }
        lastShotTimestamp = Time.time;

        OnShoot?.Invoke();

        for (int i = 0; i < firePoints.Length; i++) {
            Projectile projectile = ProjectileManager.Instance.GetProjectile();
            projectile.Initialize(projectileData, firePoints[i].position, firePoints[i].right);
        }
    }
}
