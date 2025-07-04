using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [field: SerializeField] int PlayersLives { get; set; } = 3;
    int _playersScore = 0;

    [field: SerializeField] TextMeshProUGUI LivesText { get; set; }
    [field: SerializeField] TextMeshProUGUI ScoresText { get; set; }

    public static GameSession Instance { get; private set; }

    void Awake()
    {
        // int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        // if (numGameSessions > 1)
        //     DestroyImmediate(gameObject);
        // else
        //     DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LivesText.text = PlayersLives.ToString();
        ScoresText.text = _playersScore.ToString();
    }


    public void ProcessPlayerDeath()
    {
        if (PlayersLives > 1)
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
        PlayersLives -= 1;
        LivesText.text = PlayersLives.ToString();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ResetGameSession()
    {
        ScenePersist _scenePersist = FindAnyObjectByType<ScenePersist>();
        _scenePersist.ResetScenePersist();

        SceneManager.LoadScene(0);
        // dies zerstört auch die Singleton-Instance
        Destroy(gameObject);
    }

    public void AddPointsToPlayersScore(int pointsToAdd)
    {
        _playersScore += pointsToAdd;
        ScoresText.text = _playersScore.ToString();
    }
}
