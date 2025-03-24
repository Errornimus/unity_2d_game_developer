using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] float delay = 1.0f;
    bool hasPackage = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ouuch...");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up...");
            hasPackage = true;
            Destroy(other.gameObject, delay);
        }
        else if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Package delivered...");
            hasPackage = false;
        }
    }
}
