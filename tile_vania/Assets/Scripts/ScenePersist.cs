using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        // would be better to change this to a Singleton-Instance-Pattern
        // still leaving it this way because it was used during the course
        int numScenePersists = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;

        if (numScenePersists > 1)
            DestroyImmediate(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
