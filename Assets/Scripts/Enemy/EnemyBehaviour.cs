using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private EnemyHealth enemyHealth;

    public UnityEvent OnInit;

    public PlayerCommonData PlayerCommonData => playerCommonData;

    public virtual void Initalize(PlayerCommonData playerCommonData) {
        this.playerCommonData = playerCommonData;
        enemyHealth.Initalize();
        OnInit?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.TryGetComponent<PlayerMovementBehaviour>(out PlayerMovementBehaviour p)) {
            return;
        }
    }
}
