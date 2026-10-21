using UnityEngine;

public class EnemyChaseBehaviour : EnemyBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Movement movement;

    void Update()
    {
        movement.ChangeVelocity(PlayerCommonData.PlayerPosition - (Vector2)transform.position, Time.deltaTime);
        rb.linearVelocity = movement.Velocity;
    }
}
