using System;
using UnityEngine;

public class HealthSystem
{
    private int maxHealth;
    private int currentHealth;

    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth; 
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth == 0)
        {            
            Die();
        }
    }

    //public void Heal(int amount)
    //{
    //    if (currentHealth <= 0) return;

    //    currentHealth += amount;
    //    currentHealth = Mathf.Min(currentHealth, maxHealth);

    //    OnHealthChanged?.Invoke(currentHealth);
    //}

    private void Die()
    {
        OnDeath?.Invoke();
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}
