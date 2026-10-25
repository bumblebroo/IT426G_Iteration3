using UnityEngine;

public class EnemyChaseBehaviour : EnemyBehaviour
{
    protected override void Update()
    {
        if (!PlayerCommonData) return;

        movement.ChangeVelocity(PlayerCommonData.PlayerPosition - (Vector2)transform.position, Time.deltaTime);
        base.Update();
    }
}