using System.Collections.Generic;
using UnityEngine;

public class JumpSmashExecutor : MonoBehaviour
{
    private JumpSmashAbility abilityData;
    private HashSet<Collider> ignoredColliders;

    public void ExecuteSmash(JumpSmashAbility ability)
    {
        abilityData = ability;
        CacheIgnoredColliders();

        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("JumpSmash");
    }

<<<<<<< HEAD
    public void OnAnimationSmashEvent()
=======
    private void CacheIgnoredColliders()
    {
        // Build a set of colliders that belong to the same entity (parents + children).
        // This ensures colliders on a parent (or child) of the object with this script are ignored.
        ignoredColliders = new HashSet<Collider>();

        foreach (var c in GetComponentsInParent<Collider>())
        {
            if (c != null) ignoredColliders.Add(c);
        }

        foreach (var c in GetComponentsInChildren<Collider>())
        {
            if (c != null) ignoredColliders.Add(c);
        }
    }

    public void OnAnimationSmashEvent() 
>>>>>>> f701581f65074aaba322fb59c267a050345dd216
    {
        if (abilityData == null) return;

        float radius = abilityData.radius;
        float damageAmount = abilityData.damageAmount;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in hitColliders)
        {
            // Skip any collider that belongs to this same entity (covers colliders on parents and children)
            if (ignoredColliders != null && ignoredColliders.Contains(hit)) { continue; }

            // Try to find PlayerHealth on the hit object or its parents (robust to collider placement)
            if (hit.GetComponentInParent<PlayerHealth>() is PlayerHealth targetHealth)
            {
                targetHealth.healthSystem.Damage(damageAmount);
            }
        }
    }
}
