using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [field: SerializeField] private AudioClip PickUpSound { get; set; }
    [field: SerializeField] int PointsForCoin { get; set; } = 100;

    GameSession _gameSession;
    bool _wasAlreadyCollected = false;

    void Start()
    {
        _gameSession = FindAnyObjectByType<GameSession>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TagEnum.Player) && !_wasAlreadyCollected)
        {
            AudioSource.PlayClipAtPoint(PickUpSound, Camera.main.transform.position);
            _gameSession.AddPointsToPlayersScore(PointsForCoin);
            _wasAlreadyCollected = true;
            Destroy(this.gameObject);
        }
    }
}
