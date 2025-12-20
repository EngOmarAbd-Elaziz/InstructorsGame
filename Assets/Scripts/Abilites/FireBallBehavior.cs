using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireBallBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damageAmount = 10f;
    private Transform target;
    [SerializeField] private GameObject owner;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.LookAt(target);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) { return; }

        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth targetHealth))
        {
            targetHealth.healthSystem.Damage(damageAmount);
            Debug.Log("Hit the target");
            Destroy(gameObject);
        }
        else { Destroy(gameObject); }
    }
}