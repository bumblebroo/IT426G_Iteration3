using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.AI;

[CreateAssetMenu(fileName = "PlayerCommonData", menuName = "Scriptable Objects/PlayerCommonData")]
public class PlayerCommonData : ScriptableObject
{
    [SerializeField]
    private Health playerHealth;

    private ScoreCounter scoreCounter;

    [SerializeField]
    private Vector2 playerPosition;

    [SerializeField]
    private Vector2 mousePosition;
    [SerializeField]
    private Vector2 mouseDir;

    public UnityEvent OnHeal => playerHealth.OnHeal;
    public UnityEvent OnDamage => playerHealth.OnDamage;
    public UnityEvent OnDeath => playerHealth.OnDeath;
    public int CurrentHealth => playerHealth.CurrentHealth;

    public UnityEvent OnScoreIncrease => scoreCounter.OnScoreIncrease;
    public int Score => scoreCounter.Score;

    public Vector2 PlayerPosition => playerPosition;

    public void Initalize() {
        playerHealth.Reset();
        scoreCounter.Reset();
    }

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

    public void SetPlayerPosition(Vector3 pos) {
        playerPosition = pos;
    }

    public void UpdateMouseDir() {
        mouseDir = mousePosition - playerPosition;
        mouseDir.Normalize();
    }

    public void TakeDamage(int dmg) {
        playerHealth.ChangeHealth(-Mathf.Abs(dmg));
    }

    public void Heal(int heal) {
        playerHealth.ChangeHealth(Mathf.Abs(heal));
    }

    public void IncreaseScore(int delta) {
        scoreCounter.AddScore(delta);
    }
}
