using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ouuch...");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered...");
    }
}
