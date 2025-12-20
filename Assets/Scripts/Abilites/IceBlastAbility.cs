using UnityEngine;
[CreateAssetMenu]
public class IceBlastAbility : AbilitesSO
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private float coneDegree = 110f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float slowFactor = 0.8f;
    [SerializeField] private float slowDebuffDuration = 2f;
    public override void Activate(GameObject parent, Transform target = null)
    {
        DetectEnemiesInConeAndDamage(parent, radius, coneDegree, damage);
    }

    public void DetectEnemiesInConeAndDamage(GameObject caster, float radius, float coneAngle, float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(caster.transform.position, radius);
        foreach (var hit in hitColliders)
        {
            if (hit.gameObject == caster) continue;

            if (hit.TryGetComponent<PlayerHealth>(out PlayerHealth targetHealth))
            {
                Vector3 casterForward = caster.transform.forward;
                Vector3 directionToTarget = (hit.transform.position - caster.transform.position).normalized;
                float forwardToEnemyAngle = Vector3.Angle(casterForward, directionToTarget);

                // Target is within the cone apply damage
                if (forwardToEnemyAngle < coneAngle / 2f) // if coneAngle is 90 we check if angle < 45
                {
                    Debug.Log("Target Hit");
                    targetHealth.healthSystem.Damage(damage);
                    if (hit.TryGetComponent<PlayerController>(out PlayerController targetController))
                    {
                        // Apply the slow effect to the target!
                        targetController.ApplySlowEffect(slowFactor, slowDebuffDuration);
                    }
                }
            }
        }
    }


    public void DrawAbilityGizmo(Transform casterTransform)
    {
        // Get the starting position and forward direction
        Vector3 origin = casterTransform.position;
        Vector3 forward = casterTransform.forward;

        // Calculate the half angle
        float halfAngle = coneDegree / 2f;

        // --- Draw the arc lines (The sides of the cone) ---

        // 1. Calculate the left-side direction vector
        // Use Quaternion.AngleAxis to rotate the forward vector by the negative half-angle
        Quaternion leftRotation = Quaternion.AngleAxis(-halfAngle, Vector3.up);
        Vector3 leftDirection = leftRotation * forward;

        // 2. Calculate the right-side direction vector
        // Rotate the forward vector by the positive half-angle
        Quaternion rightRotation = Quaternion.AngleAxis(halfAngle, Vector3.up);
        Vector3 rightDirection = rightRotation * forward;

        // Draw the two straight lines from the caster to the edge of the range
        Gizmos.DrawRay(origin, leftDirection * radius);
        Gizmos.DrawRay(origin, rightDirection * radius);

        // --- Draw the circular cap (The arc at the end) ---
        // We will draw a series of small segments to approximate the curve

        int segments = 20;
        Vector3 previousPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            // Map the segment index (0 to 20) to an angle (from -halfAngle to halfAngle)
            float angle = Mathf.Lerp(-halfAngle, halfAngle, (float)i / segments);

            // Calculate the direction vector for this segment
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 direction = rotation * forward;

            // Calculate the position of this point on the arc
            Vector3 currentPoint = origin + (direction * radius);

            if (i > 0)
            {
                // Draw a line connecting the previous point to the current point
                Gizmos.DrawLine(previousPoint, currentPoint);
            }

            previousPoint = currentPoint;
        }
    }

}
