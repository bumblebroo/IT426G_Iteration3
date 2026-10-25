using System.Linq;
using UnityEngine;

public class EnemyDodgeBehaviour : EnemyBehaviour
{
    [SerializeField]
    private float dodgeForce;

    [SerializeField]
    private float projectileDodgeRange;

    [SerializeField]
    private LayerMask projectileLayer;
    public float ProjectileDodgeRange => projectileDodgeRange;

    protected override void Update() {
        if (!PlayerCommonData) return;

        Vector2 playerDir = PlayerCommonData.PlayerPosition - (Vector2)transform.position;
        playerDir.Normalize();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, projectileDodgeRange, projectileLayer);
        Vector2 pos = transform.position;
        Vector2 force = pos.SeparationForce(colliders.Select(c => (Vector2)c.transform.position).ToArray(), dodgeForce);

        Vector2 perpendicularForce = force - (Vector2.Dot(playerDir, force) * playerDir);

        movement.ChangeVelocity(perpendicularForce.normalized * force.magnitude + playerDir, Time.deltaTime);
        base.Update();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projectileDodgeRange);
    }
}
