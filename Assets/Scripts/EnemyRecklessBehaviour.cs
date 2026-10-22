using UnityEngine;

public class EnemyRecklessBehaviour : EnemyBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float directionSwitchCD;

    private float directionSwitchTimestamp;

    private Vector2 dir = new Vector2();

    void Update() {
        if(Time.time - directionSwitchTimestamp > directionSwitchCD) {
            dir = PlayerCommonData.PlayerPosition - (Vector2)transform.position;
            directionSwitchTimestamp = Time.time;
        }

        movement.ChangeVelocity(dir, Time.deltaTime);
        rb.linearVelocity = movement.Velocity;
    }
}
