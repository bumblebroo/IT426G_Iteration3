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

    private float lastTimeStamp = 0;

    public float Speed => speed;
    public float Acceleration => acceleration;
    public Vector2 Velocity => velocity;
    public Movement(float speed, float acceleration) {
        this.speed = Mathf.Abs(speed);
        this.acceleration = Mathf.Abs(acceleration);
    }

    public void ChangeVelocity(Vector2 dir, float deltaTime) {
        if(Time.realtimeSinceStartup == lastTimeStamp) {
            return;
        }

        lastTimeStamp = Time.time;
        velocity = Vector2.MoveTowards(velocity, dir.normalized * speed, speed * (Mathf.Abs(deltaTime) / acceleration));
    }
}
