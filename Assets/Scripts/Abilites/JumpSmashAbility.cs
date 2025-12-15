using UnityEngine;
[CreateAssetMenu]
public class JumpSmashAbility : AbilitesSO
{
    public float jumpStrength = 10f;
    public override void Activate(GameObject parent, Transform target = null)
    {
        Rigidbody rigidbody = parent.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.up * jumpStrength , ForceMode.Impulse);
    }
}
