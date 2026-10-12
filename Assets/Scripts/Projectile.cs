using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectileData projectileData;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Rigidbody2D rb;

    [Space]

    [SerializeField]
    private float lifeTime = 10f;

    public void Initialize(ProjectileData projectileData, Vector2 pos, Vector3 dir) {
        this.projectileData = projectileData;

        transform.position = pos;
        transform.up = dir;

        sr.sprite = projectileData.Sprite;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(transform.up * projectileData.Speed, ForceMode2D.Impulse);

        StartCoroutine(ReleaseAfterDelay());
    }

    public void DeInitialize() {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(projectileData.Damage);
        }

        Debug.Log($"HitSmth: {collision.name}");
        ProjectileManager.Instance.ReleaseProjectile(this);
    }

    private IEnumerator ReleaseAfterDelay() {
        yield return new WaitForSeconds(lifeTime);
        ProjectileManager.Instance.ReleaseProjectile(this);
    }
}
