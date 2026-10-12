using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private Transform pivot, firePoint;

    [SerializeField]
    private WeaponData weaponData;

    private float lastShotTimestamp;

    private Vector2 mouseScreenPos;

    void Update()
    {
        playerCommonData.SetMousePosition(mouseScreenPos);
        playerCommonData.SetMouseDir(transform.position);

        float angle = Mathf.Atan2(playerCommonData.MouseDir.y, playerCommonData.MouseDir.x);
        pivot.rotation = new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));
        Shoot();
    }

    public void MousePositionInput(InputAction.CallbackContext context) {
        mouseScreenPos = context.ReadValue<Vector2>();
    }

    private void Shoot() {
        if (!weaponData) {
            Debug.LogWarning("Missing weapon");
            return;
        }

        if(Time.time - lastShotTimestamp < 1 / weaponData.RPS) {
            return;
        }

        lastShotTimestamp = Time.time;
        Projectile projectile = ProjectileManager.Instance.GetProjectile();
        projectile.Initialize(weaponData.ProjectileData, firePoint.position, firePoint.right);
    }
}
