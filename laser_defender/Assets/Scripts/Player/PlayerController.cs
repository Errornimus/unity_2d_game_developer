using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Movement with Boundaries
    [Header("Movement")]
    [SerializeField] float MovementSpeed = 6.5f;

    [Header("Viewport Boundary Padding")]
    [SerializeField] float _paddingLeft;
    [SerializeField] float _paddingRight;
    [SerializeField] float _paddingTop;
    [SerializeField] float _paddingBottom;

    Vector2 _minViewportBoundary;
    Vector2 _maxViewportBoundary;

    // Input System
    private Vector2 _rawInput;

    /* 
        Unity 
    */
    private void Update()
    {
        MovePlayer();
    }

    private void Start()
    {
        InitBounds();
    }

    /* 
        Logic
    */
    private void MovePlayer()
    {
        Vector2 delta = _rawInput * MovementSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();

        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, _minViewportBoundary.x + _paddingLeft, _maxViewportBoundary.x - _paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, _minViewportBoundary.y + _paddingBottom, _maxViewportBoundary.y - _paddingTop);

        transform.position = newPosition;
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        _minViewportBoundary = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0f));
        _maxViewportBoundary = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f));
    }

    /* 
        EVENTS
    */
    private void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
        Debug.Log(_rawInput);
    }
}
