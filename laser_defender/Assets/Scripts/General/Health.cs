using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints { get; private set; } = 50;

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.Damage);
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage)
    {
        HealthPoints -= damage;

        if (HealthPoints <= 0)
            Destroy(gameObject);
    }
}
