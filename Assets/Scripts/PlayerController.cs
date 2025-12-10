using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private float rotationSpeed = 600f;
    private Vector3 airMoveDirection = Vector3.zero;
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


        if (IsGrounded())
        {

            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            if (moveDir != Vector3.zero)
                moveDir = moveDir.normalized;


            airMoveDirection = moveDir * moveSpeed;


            transform.position += moveDir * moveSpeed * Time.deltaTime;

            isWalking = moveDir != Vector3.zero;
        }


        else
        {

            transform.position += airMoveDirection * Time.deltaTime;


        }
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
