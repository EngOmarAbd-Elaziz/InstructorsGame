using UnityEngine;

[CreateAssetMenu]
public class FireBallAbility : AbilitesSO
{
    public GameObject fireBallPrefab;
    public float velocity;

    public override void Activate(GameObject parent)
    {
        Instantiate(fireBallPrefab , parent.transform.position,
                    fireBallPrefab.transform.rotation, parent.transform);
    }
}
