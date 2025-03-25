using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] float delay = 1.0f;
    [SerializeField] Color32 hasPackageColor = new Color32(130, 188, 119, 255);
    [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);

    bool hasPackage = false;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, delay);
        }
        else if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Package delivered...");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }
    }
}
