using UnityEngine;

[CreateAssetMenu]
public class FireBallAbility : AbilitesSO
{
    public GameObject fireBallPrefab;
    public float velocity;

    public override void Activate(GameObject parent)
    {
        Instantiate(fireBallPrefab , parent.transform.position + new Vector3(0 , 0 , 3),
                    fireBallPrefab.transform.rotation, parent.transform);
    }
}
