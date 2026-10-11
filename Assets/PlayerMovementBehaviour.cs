using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private Movement movement;

    private Vector2 input;

    private void Update() {
        movement.ChangeVelocity(input, Time.deltaTime);
        transform.position += (Vector3)movement.Velocity * Time.deltaTime;
    }

    public void TakeInput(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>().normalized;
    }
}
