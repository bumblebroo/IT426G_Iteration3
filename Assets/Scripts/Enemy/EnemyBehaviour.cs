using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private EnemyHealth enemyHealth;

    [SerializeField]
    protected Movement movement;

    public UnityEvent OnInit;

    public PlayerCommonData PlayerCommonData => playerCommonData;

    public virtual void Initalize(PlayerCommonData playerCommonData) {
        this.playerCommonData = playerCommonData;
        enemyHealth.Initalize();
        movement.Reset();
        OnInit?.Invoke();
    }
    protected virtual void Update() {
        rb.linearVelocity = movement.Velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.CompareTag("Player")) {
            return;
        }

        if(enemyHealth.CurrentHealth == 0) {
            return;
        }

        playerCommonData.TakeDamage(1);
        enemyHealth.TakeDamage(int.MaxValue);
    }
}
