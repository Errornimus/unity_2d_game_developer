using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [field: Header("Movement")]
    [field: SerializeField] float runSpeed { get; set; } = 3.0f;
    Vector2 moveInput { get; set; }
    Rigidbody2D playerRigidBody;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidBody.linearVelocityY);
        playerRigidBody.linearVelocity = playerVelocity;

        FlipSpriteAccordingToMovementDirection();
    }

    void FlipSpriteAccordingToMovementDirection()
    {
        // alternative way
        // bool playerHasHorizontalSpeed = playerRigidBody.linearVelocityX != 0;
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.linearVelocityX) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.linearVelocityX), 1.0f);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
}
