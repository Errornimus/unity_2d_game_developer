using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GingerMovement : MonoBehaviour
{
    /*
     * Try Converting to PlayerState-Mechanism and checking Movement again or Enum-Switch-Construct
     * afterwards try InputComponent
    */
    [field: Header("Movement")]
    [field: SerializeField] float _runSpeed { get; set; } = 7.0f;
    [field: SerializeField] float _jumpSpeed { get; set; } = 5.0f;
    [field: SerializeField] float _climbingSpeed { get; set; } = 5.0f;

    [field: Header("Combat")]
    [field: SerializeField] GameObject _arrowPrefab { get; set; }
    [field: SerializeField] Transform _arrowSpawnPoint { get; set; }
    [field: SerializeField] Vector2 _deathAnimation = new Vector2(0f, 10f);

    Vector2 _moveInput { get; set; }

    Rigidbody2D _rigidBody;
    // CapsuleCollider2D _bodyCollider;
    BoxCollider2D _feetCollider;
    float _startingGravityScale;

    Animator _playerAnimator;

    bool _isAlive { get; set; }

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
        // _bodyCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        _startingGravityScale = _rigidBody.gravityScale;

        _isAlive = true;
    }

    void Update()
    {
        if (!_isAlive)
            return;

        Run();
        ClimbLadder();
        Die();
    }

    void Die()
    {
        if (_rigidBody.IsTouchingLayers(LayerConstants.EnemyMask) || _rigidBody.IsTouchingLayers(LayerConstants.HazardMask))
        {
            _isAlive = false;
            _playerAnimator.SetTrigger(GingerParams.Dying);
            _rigidBody.linearVelocity = _deathAnimation;
        }
    }

    void ClimbLadder()
    {
        if (IsClimbingAllowed())
        {
            // alternative way
            // _rigidBody.linearVelocityY = _moveInput.y * _climbingSpeed;
            Vector2 climbingVelocity = new Vector2(_rigidBody.linearVelocityX, _moveInput.y * _climbingSpeed);
            _rigidBody.linearVelocity = climbingVelocity;

            _rigidBody.gravityScale = 0;
            _playerAnimator.SetBool(GingerParams.isClimbing, HasVerticalSpeed());
        }
        else
        {
            _rigidBody.gravityScale = _startingGravityScale;
            _playerAnimator.SetBool(GingerParams.isClimbing, false);
        }

    }

    bool HasVerticalSpeed()
    {
        return Mathf.Abs(_rigidBody.linearVelocityY) > Mathf.Epsilon;
    }

    bool IsClimbingAllowed()
    {
        return _feetCollider.IsTouchingLayers(LayerConstants.LadderMask);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * _runSpeed, _rigidBody.linearVelocityY);
        _rigidBody.linearVelocity = playerVelocity;

        if (HasHorizontalSpeed())
        {
            FlipSprite();
            _playerAnimator.SetBool(GingerParams.isRunning, true);
        }
        else
            _playerAnimator.SetBool(GingerParams.isRunning, false);
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
        return _feetCollider.IsTouchingLayers(LayerConstants.GroundMask) && _isAlive;
    }

    void OnAttack(InputValue value)
    {
        if (!_isAlive) return;

        GameObject arrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.Euler(0, 0, -42));

    }
}
