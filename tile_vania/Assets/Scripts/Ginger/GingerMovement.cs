using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GingerMovement : MonoBehaviour
{
    [field: Header("Movement")]
    [field: SerializeField] float _runSpeed { get; set; } = 7.0f;
    [field: SerializeField] float _jumpSpeed { get; set; } = 5.0f;
    [field: SerializeField] float _climbingSpeed { get; set; } = 5.0f;

    Vector2 _moveInput { get; set; }
    Rigidbody2D _rigidBody;
    CapsuleCollider2D _capsuleCollider;

    Animator _playerAnimator;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        Run();
        ClimbLadder();
    }

    void ClimbLadder()
    {
        if (IsClimbingAllowed())
        {
            // alternative way
            // _rigidBody.linearVelocityY = _moveInput.y * _climbingSpeed;
            Vector2 climbingVelocity = new Vector2(_rigidBody.linearVelocityX, _moveInput.y * _climbingSpeed);
            _rigidBody.linearVelocity = climbingVelocity;
        }

        _playerAnimator.SetBool(GingerParams.isClimbing, HasVerticalSpeed() && IsClimbingAllowed());
    }

    bool HasVerticalSpeed()
    {
        return Mathf.Abs(_rigidBody.linearVelocityY) > Mathf.Epsilon;
    }

    bool IsClimbingAllowed()
    {
        return _capsuleCollider.IsTouchingLayers(LayerConstants.LadderMask);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * _runSpeed, _rigidBody.linearVelocityY);
        _rigidBody.linearVelocity = playerVelocity;

        FlipSprite();

        _playerAnimator.SetBool(GingerParams.isRunning, HasHorizontalSpeed());
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(_rigidBody.linearVelocityX), 1.0f);
    }

    bool HasHorizontalSpeed()
    {
        // alternative way
        // bool playerHasHorizontalSpeed = playerRigidBody.linearVelocityX != 0;
        return Mathf.Abs(_rigidBody.linearVelocityX) > Mathf.Epsilon;
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        Debug.Log(_moveInput);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && IsJumpingAllowed())
            _rigidBody.linearVelocity += new Vector2(0f, _jumpSpeed);
    }

    bool IsJumpingAllowed()
    {
        return _capsuleCollider.IsTouchingLayers(LayerConstants.GroundMask);
    }
}
