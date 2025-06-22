using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [field: SerializeField] int _playersLives { get; set; } = 3;

    void Awake()
    {
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        if (numGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void ProcessPlayerDeath()
    {
        if (_playersLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        _playersLives -= 1;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
