using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private EnemyHealth enemyHealth;

    public PlayerCommonData PlayerCommonData => playerCommonData;

    public virtual void Initalize(PlayerCommonData playerCommonData) {
        this.playerCommonData = playerCommonData;
        enemyHealth.Initalize();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.TryGetComponent<PlayerMovementBehaviour>(out PlayerMovementBehaviour p)) {
            return;
        }
    }
}
