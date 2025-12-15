using UnityEngine;

[CreateAssetMenu]
public class FireBallAbility : AbilitesSO
{
    public GameObject fireBallPrefab;
    public float spawnDistance = 1f;
    public float appearTime = 3f;

    public override void Activate(GameObject parent , Transform target = null)
    {
        Vector3 spawnDirection;
        if (target != null)
        {
            spawnDirection = (target.position - parent.transform.position).normalized;
        }
        else { spawnDirection = parent.transform.position; }

        Vector3 spawnPosition = parent.transform.position + spawnDirection * spawnDistance;
        Quaternion spawnRotation = Quaternion.LookRotation(spawnDirection);

        GameObject fireball = Instantiate(fireBallPrefab, spawnPosition, spawnRotation);
        Destroy(fireball, appearTime);
        
        FireBallBehavior fireBallBehavior = fireball.GetComponent<FireBallBehavior>();
        
        if(fireBallBehavior != null) 
        {
            fireBallBehavior.SetOwner(parent);
            if (target != null) 
            {
                fireBallBehavior.SetTarget(target);
            }
        }
    }
}