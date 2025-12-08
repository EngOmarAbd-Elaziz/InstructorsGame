using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private float rotationSpeed = 300f;
    private bool isWalking;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        Vector3 moveDir = transform.forward * inputVector.y;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, inputVector.x * rotationSpeed * Time.deltaTime);

        isWalking = inputVector.y != 0;

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void HandleJump()
    {
        if (gameInput.IsJumpPressed() && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
