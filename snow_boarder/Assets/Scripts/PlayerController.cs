using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torgueAmount = 1.0f;
    [SerializeField] float boostSpeed = 20.0f;
    [SerializeField] float baseSpeed = 8.0f;
    Rigidbody2D rb2d = null;
    SurfaceEffector2D se2d = null;
    bool Crashed { get; set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        se2d = FindFirstObjectByType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Crashed)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            se2d.speed = boostSpeed;
        else
            se2d.speed = baseSpeed;
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            rb2d.AddTorque(torgueAmount);
        else if (Input.GetKey(KeyCode.RightArrow))
            rb2d.AddTorque(-torgueAmount);
    }

    public void DisableControls()
    {
        Crashed = true;
    }
}
