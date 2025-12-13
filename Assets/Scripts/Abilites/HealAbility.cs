using UnityEngine;

[CreateAssetMenu]
public class HealAbility : AbilitesSO
{
    private HealthBarUI healthBarUI;
    private float healAmount = 30f;
    public override void Activate(GameObject parent)
    {
        healthBarUI.healthSystem.Heal(healAmount);
    }
}
