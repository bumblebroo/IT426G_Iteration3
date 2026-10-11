using UnityEngine;

public class Movement
{
    private float speed;
    private float acceleration;

    private Vector2 velocity;

    public Vector2 Velocity => velocity;
    public Movement(float speed, float acceleration) {
        this.speed = Mathf.Abs(speed);
        this.acceleration = Mathf.Abs(acceleration);
    }

    public void ChangeVelocity(Vector2 dir, float deltaTime) {
        velocity = Vector2.MoveTowards(velocity, dir.normalized * speed, speed * (Mathf.Abs(deltaTime) / acceleration));
    }
}
