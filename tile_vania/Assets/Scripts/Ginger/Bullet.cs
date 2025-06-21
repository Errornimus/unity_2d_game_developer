using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] float _bulletSpeed = 4.0f;
    float _xSpeed;

    Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GingerMovement _player = FindAnyObjectByType<GingerMovement>();
        _xSpeed = _player.transform.localScale.x;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        _rigidBody.linearVelocityX = Mathf.Sign(_xSpeed) * _bulletSpeed;
        transform.localScale = new Vector2(Mathf.Sign(_xSpeed), Mathf.Sign(_xSpeed));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TagEnum.Enemy))
            Destroy(collision.gameObject);

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
