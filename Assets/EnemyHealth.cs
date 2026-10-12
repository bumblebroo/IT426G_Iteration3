using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private Health health;

    public void TakeDamage(int damage) {
        health.ChangeHealth(-Mathf.Abs(damage));
    }
}
