using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private Health health;

    public int CurrentHealth => health.CurrentHealth;
    public UnityEvent OnHit => health.OnDamage;
    public UnityEvent OnDeath => health.OnDeath;
    
    public void Initalize() {
        health.Reset();
    }
    public void TakeDamage(int damage) {
        health.ChangeHealth(-Mathf.Abs(damage));
    }
}
