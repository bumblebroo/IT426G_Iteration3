using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Health
{
    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    public UnityEvent OnDamage = new UnityEvent();
    public UnityEvent OnHeal = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();

    public bool hasDied;

    public int CurrentHealth => currentHealth;

    public void Reset() {
        currentHealth = maxHealth;
        hasDied = false;
    }

    public Health(int maxHealth) {
        maxHealth = Mathf.Abs(maxHealth);
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        hasDied = false;
    }

    public void ChangeHealth(int healthDelta) {
        currentHealth += healthDelta;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(healthDelta < 0) {
            OnDamage?.Invoke();
        } else if(healthDelta > 0) {
            OnHeal?.Invoke();
        }

        if(currentHealth == 0 && !hasDied) {
            OnDeath?.Invoke();
            hasDied = true;
        }
    }
}
