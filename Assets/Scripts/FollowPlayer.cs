using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        Vector3 targetPos = player.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);

        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
