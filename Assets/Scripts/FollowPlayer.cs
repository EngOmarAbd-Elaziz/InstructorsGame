using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public float distance = 4f;
    public float height = 2f;
    public float smoothSpeed = 10f;

    void LateUpdate()
    {

        Vector3 targetPos = player.position - player.forward * distance + Vector3.up * height;


        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);

        Vector3 lookTarget = player.position + player.forward * 5f + Vector3.up * 1.5f;
        transform.LookAt(lookTarget);
    }
}
