using System;
using UnityEngine;

[Serializable]
public class Movement
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float acceleration;

    private Vector2 velocity;

    public float Speed => speed;
    public float Acceleration => acceleration;
    public Vector2 Velocity => velocity;
    public Movement(float speed, float acceleration) {
        this.speed = Mathf.Abs(speed);
        this.acceleration = Mathf.Abs(acceleration);
    }

    public void Reset() {
        velocity = Vector2.zero;
    }

    public void ChangeVelocity(Vector2 dir, float deltaTime) {
        velocity = Vector2.MoveTowards(velocity, dir.normalized * speed, speed * (Mathf.Abs(deltaTime) / acceleration));
    }
}
