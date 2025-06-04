using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    Vector2 moveInput { get; set; }
    Rigidbody2D rigidBody;
    [field: SerializeField] float runSpeed { get; set; } = 3.0f;

    void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody.linearVelocityY);
        rigidBody.linearVelocity = playerVelocity;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
}
