using UnityEngine;

[CreateAssetMenu]
public class HealAbility : AbilitesSO
{
    private float healAmount = 30f;
    public override void Activate(GameObject parent)
    {
       HealthBarUI healthBarUI = parent.GetComponent<PlayerController>().PlayerData.HealthBarUI;
        healthBarUI.healthSystem.Heal(healAmount);
    }
}
