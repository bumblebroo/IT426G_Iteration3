using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private PlayerWeapon playerWeapon;

    private Vector2 mouseScreenPos;

    void Update()
    {
        playerCommonData.SetMousePosition(mouseScreenPos);
        playerCommonData.SetPlayerPosition(transform.position);
        playerCommonData.UpdateMouseDir();

        float angle = Mathf.Atan2(playerCommonData.MouseDir.y, playerCommonData.MouseDir.x);
        pivot.rotation = new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));
        playerWeapon.Shoot();
    }

    public void MousePositionInput(InputAction.CallbackContext context) {
        mouseScreenPos = context.ReadValue<Vector2>();
    }
}
