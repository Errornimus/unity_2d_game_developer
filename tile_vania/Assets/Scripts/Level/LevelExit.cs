using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [field: SerializeField] float _levelLoadDelay { get; set; } = 1.0f;

    ScenePersist _scenePersist;
    bool _levelAlreadyLoaded = false;

    void Start()
    {
        _scenePersist = FindAnyObjectByType<ScenePersist>();
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(_levelLoadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        _scenePersist.ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TagEnum.Player) && !_levelAlreadyLoaded)
        {
            _levelAlreadyLoaded = true;
            StartCoroutine(LoadNextLevel());
        }
    }
}
