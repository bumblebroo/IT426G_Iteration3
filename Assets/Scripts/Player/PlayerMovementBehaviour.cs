using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Movement movement;

    private Vector2 input;

    public float Speed => movement.Speed;
    public float Acceleration => movement.Acceleration;
    public Vector2 CurrentVelocity => movement.Velocity;

    private void Update() {
        movement.ChangeVelocity(input, Time.deltaTime);
        rb.linearVelocity = (Vector3)movement.Velocity;
    }

    public void TakeInputRaw(Vector2 input) {
        this.input = input;
    }

    public void TakeInput(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>().normalized;
    }
}
