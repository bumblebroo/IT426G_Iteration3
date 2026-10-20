using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Movement movement;

    private Vector2 input;

    private void Update() {
        movement.ChangeVelocity(input, Time.deltaTime);
        rb.linearVelocity = (Vector3)movement.Velocity;
    }

    public void TakeInput(Vector2 input) {
        this.input = input;
    }

    public void TakeInput(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>().normalized;
    }
}
