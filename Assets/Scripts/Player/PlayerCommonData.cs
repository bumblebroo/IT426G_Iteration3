using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCommonData", menuName = "Scriptable Objects/PlayerCommonData")]
public class PlayerCommonData : ScriptableObject
{
    [SerializeField]
    private Health playerHealth;

    [SerializeField]
    private Vector2 playerPosition;

    [SerializeField]
    private Vector2 mousePosition;
    [SerializeField]
    private Vector2 mouseDir;

    public Vector2 PlayerPosition => playerPosition;

    public Vector2 MouseDir {
        get {
            return mouseDir;
        }
    }

    public Vector2 MousePosition {
        get {
            return mousePosition;
        }
    }

    public void SetMousePosition(Vector2 mouseScreenPosition) {
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Mathf.Abs(Camera.main.transform.position.z)));
    }

    public void SetMouseDir(Vector2 playerPosition) {
        this.playerPosition = playerPosition;
        mouseDir = mousePosition - playerPosition;
        mouseDir.Normalize();
    }
}
