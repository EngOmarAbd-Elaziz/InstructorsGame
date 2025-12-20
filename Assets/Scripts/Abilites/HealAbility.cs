using UnityEngine;

[CreateAssetMenu]
public class HealAbility : AbilitesSO
{
    private float healAmount = 30f;
    public override void Activate(GameObject parent, Transform target = null)
    {
        PlayerHealth playerHealth = parent.GetComponent<PlayerHealth>();
        playerHealth.healthSystem.Heal(healAmount);
    }
}
