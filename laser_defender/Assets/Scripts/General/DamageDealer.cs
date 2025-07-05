using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [field: SerializeField] public int Damage { get; private set; } = 10;

    public void Hit()
    {
        Destroy(gameObject);
    }
}