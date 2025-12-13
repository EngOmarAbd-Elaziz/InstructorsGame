using UnityEngine;

public class FireBallBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);   
    }
}
