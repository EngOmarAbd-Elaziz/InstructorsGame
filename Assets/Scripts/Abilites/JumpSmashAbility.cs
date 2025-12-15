using UnityEngine;
[CreateAssetMenu]
public class JumpSmashAbility : AbilitesSO
{
    public float radius = 8f;
    public float damageAmount = 40f;
    private Animator anim;
    public override void Activate(GameObject parent, Transform target = null)
    {
        JumpSmashExecutor executor = parent.GetComponentInChildren<JumpSmashExecutor>();

        if (executor != null) 
        {
            executor.ExecuteSmash(this);
        }
        else { Debug.Log("JumpSmashExecutor not found on " + parent.name); }
    }

    public void DrawAbilityGizmo(Transform casterTransform)
    {
        // The position of the explosion is the player's position on the ground
        Vector3 center = casterTransform.position;

        // Ensure the circle is drawn flat on the ground (y=0) if the player is in the air
        // We'll use the current position, assuming the gizmo is checked when the player is on the ground.

        // Draw a wire sphere at the caster's position with the defined radius
        // This clearly marks the boundary of the AOE.
        Gizmos.DrawWireSphere(center, radius);

        // OPTIONAL: Draw a small line to the center for clarity
        Gizmos.DrawLine(center + Vector3.up * 0.1f, center + Vector3.up * 0.1f + casterTransform.forward * radius);
    }
}