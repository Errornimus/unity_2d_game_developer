using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [field: SerializeField] float MovementSpeed { get; set; } = 6.5f;

    private Vector2 _rawInput;

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 delta = _rawInput * MovementSpeed * Time.deltaTime;
        transform.position += delta;
    }

    private void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
        Debug.Log(_rawInput);
    }
}
