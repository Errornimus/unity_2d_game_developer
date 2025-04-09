using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
            Debug.Log("Boink");
    }
}
