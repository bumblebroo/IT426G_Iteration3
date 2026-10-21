using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private Health health;

    public UnityEvent OnHit => health.OnDamage;
    public UnityEvent OnDeath => health.OnDeath;
    public void TakeDamage(int damage) {
        health.ChangeHealth(-Mathf.Abs(damage));
    }
}
