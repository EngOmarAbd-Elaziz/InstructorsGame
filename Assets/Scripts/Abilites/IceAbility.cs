using UnityEngine;
[CreateAssetMenu]
public class IceAbility : AbilitesSO
{
    public GameObject cubeTest;
    public override void Activate(GameObject parent, Transform target = null)
    {
        Instantiate(cubeTest , parent.transform.position ,
            cubeTest.transform.rotation , parent.transform);
    }
}
