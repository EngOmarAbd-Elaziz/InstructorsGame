using UnityEngine;

public class AbilityGizmosDraw : MonoBehaviour
{
    [SerializeField] private IceBlastAbility iceBlastSO;

    // The OnDrawGizmos method is called automatically in the Editor
    private void OnDrawGizmos()
    {
        if (iceBlastSO == null) return;

        // Ensure the Gizmos are drawn only when the object is selected
        // We use OnDrawGizmosSelected for this
        // Note: You must select the player object in the Hierarchy to see this Gizmo

        Gizmos.color = Color.cyan; // Set the color of the cone

        // Call the drawing function from the Ability SO
        iceBlastSO.DrawAbilityGizmo(transform);
    }
}
