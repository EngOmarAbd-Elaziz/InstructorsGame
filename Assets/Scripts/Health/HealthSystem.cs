public class HealthSystem
{
    private float health;
    private float healthMax;
    public HealthSystem(int healthMax) {this.health = healthMax; this.healthMax = healthMax;}
    public float GetHealth(){ return health; } 
    public float GetHealthMax(){ return healthMax; } 
    public float GetHealthPercent(){ return health / healthMax; } 
    public void Damage(int damageAmount) 
    {
        health -= damageAmount;
        if (health < 0) { health = 0; }
    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) { health = healthMax; }
    }
}