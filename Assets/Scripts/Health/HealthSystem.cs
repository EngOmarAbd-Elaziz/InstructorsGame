using System;
using UnityEngine;
[System.Serializable]
public class HealthSystem
{
    private float health;
    private float healthMax;
    public event EventHandler OnHealthChanged;
    public event EventHandler OnLowHealthReached;
    private bool isLowHealth = false;
    public HealthSystem(float healthMax) { this.health = healthMax; this.healthMax = healthMax; }
    public float GetHealth() { return health; }
    public float GetHealthMax() { return healthMax; }
    public float GetHealthPercent() { return health / healthMax; }
    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0) { health = 0; }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        CheckHealthStatus();
    }
    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > healthMax) { health = healthMax; }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        CheckHealthStatus();
    }
    private void CheckHealthStatus()
    {
        float healthPercent = GetHealthPercent();
        if (healthPercent < 0.25f && isLowHealth == false)
        {
            isLowHealth = true;
            OnLowHealthReached?.Invoke(this, EventArgs.Empty);
            Debug.Log("Player is now in low health");
        }
        else if (healthPercent >= 0.25f && isLowHealth == true)
        {
            isLowHealth = false;
            // could add an OnRecoveredFromLowHealth event here if needed
        }
    }
}