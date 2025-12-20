using UnityEngine;

public class JumpSmashExecutor : MonoBehaviour
{
    private JumpSmashAbility abilityData;

    public void ExecuteSmash(JumpSmashAbility ability)
    {
        abilityData = ability;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("JumpSmash");
    }

    public void OnAnimationSmashEvent()
    {
        float radius = abilityData.radius;
        float damageAmount = abilityData.damageAmount;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in hitColliders)
        {
            if (hit.gameObject == gameObject) { continue; }
            if (hit.TryGetComponent<PlayerHealth>(out PlayerHealth targetHealth))
            {
                targetHealth.healthSystem.Damage(damageAmount);
            }
        }
    }
}
