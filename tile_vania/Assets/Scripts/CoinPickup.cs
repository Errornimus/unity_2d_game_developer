using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TagEnum.Player))
            Destroy(this.gameObject);
    }
}
