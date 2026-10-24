using UnityEngine;

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

    [Header("SFX")]

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private float volume;

    [SerializeField]
    private float volumeVariation;

    [SerializeField]
    private float pitch;

    [SerializeField]
    private float pitchVariation;

    public void Shoot() {
        if (Time.time - lastShotTimestamp < 1 / rps) {
            return;
        }
        lastShotTimestamp = Time.time;

        sfxHandler.PlaySFX(SFXHandler.PrepareGlobalSFXInfoWithVariation(audioClips, volume, volumeVariation, pitch, pitchVariation));

        for (int i = 0; i < firePoints.Length; i++) {
            Projectile projectile = ProjectileManager.Instance.GetProjectile();
            projectile.Initialize(projectileData, firePoints[i].position, firePoints[i].right);
        }
    }
}
