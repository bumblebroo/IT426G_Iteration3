using System.Linq;
using UnityEngine;

public class EnemyDodgeBehaviour : EnemyBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float dodgeForce;

    [SerializeField]
    private float projectileDodgeRange;

    [SerializeField]
    private LayerMask projectileLayer;

    [SerializeField]
    private Movement movement;

    public float ProjectileDodgeRange => projectileDodgeRange;

    void Update() {
        Vector2 playerDir = PlayerCommonData.PlayerPosition - (Vector2)transform.position;
        playerDir.Normalize();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, projectileDodgeRange, projectileLayer);
        Vector2 pos = transform.position;
        Vector2 force = pos.SeparationForce(colliders.Select(c => (Vector2)c.transform.position).ToArray(), dodgeForce);

        Vector2 perpendicularForce = force - (Vector2.Dot(playerDir, force) * playerDir);

        movement.ChangeVelocity(perpendicularForce.normalized * force.magnitude + playerDir, Time.deltaTime);
        rb.linearVelocity = movement.Velocity;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projectileDodgeRange);
    }
}
