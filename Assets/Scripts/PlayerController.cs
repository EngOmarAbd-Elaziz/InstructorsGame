using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 0.5f;
    private float moveSpeed;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameInput gameInput;
    private Vector2 moveInput;
    private Rigidbody rigidbody;
    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    private void Start()
    {
        //player.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        rigidbody = player.GetComponent<Rigidbody>();
        moveSpeed = baseMoveSpeed;
    }

    private void Update()
    {
        if (!GameManger.Instance.IsGamePlaying() || GameManger.Instance.IsGameOver()
            || GameManger.Instance.isGamePaused) { return; }
        
        Vector2 moveInput = gameInput.GetMovementVectorNormalized();
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (moveDirection.magnitude >= 0.1f)
        {
            lastMoveDirection = moveDirection;
        }
        Quaternion targetRotation = Quaternion.LookRotation(lastMoveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                             targetRotation, Time.deltaTime * rotationSpeed);
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        
    }
    private void FixedUpdate()
    {
        if (!GameManger.Instance.IsGamePlaying() || GameManger.Instance.IsGameOver()
            || GameManger.Instance.isGamePaused) { return; }
        rigidbody.linearVelocity = moveDirection * moveSpeed;
    }


    public void ApplySlowEffect(float slowFactor, float duration)
    {
        StartCoroutine(HandleSlowDebuff(slowFactor, duration));
    }
    private IEnumerator HandleSlowDebuff(float slowFactor, float duration)
    {
        moveSpeed = baseMoveSpeed * slowFactor;
        yield return new WaitForSeconds(duration);
        moveSpeed = baseMoveSpeed;
    }
}
