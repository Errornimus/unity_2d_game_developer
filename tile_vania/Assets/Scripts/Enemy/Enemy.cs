using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] float _moveSpeed = 1.0f;
    Rigidbody2D _rigidBody;
    Transform _transform;


    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        move();
    }

    void move()
    {
        _rigidBody.linearVelocityX = _moveSpeed;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _moveSpeed = -_moveSpeed;
        FlipSprite();
    }

    private void FlipSprite()
    {
        _transform.localScale = new Vector2(-(Mathf.Sign(_rigidBody.linearVelocityX)), 1.0f);
    }
}
