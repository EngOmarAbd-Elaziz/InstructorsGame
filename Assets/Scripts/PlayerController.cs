using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameInput gameInput;
    private Vector2 moveInput;
    private Rigidbody rigidbody;
    private Vector3 move;
    private void Start()
    {
        player.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        rigidbody = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 moveInput = gameInput.GetMovementVectorNormalized();
        move = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(move * moveSpeed ,ForceMode.VelocityChange);
    }
}
