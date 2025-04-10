using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float finishingDelay = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Invoke("ReloadScene", finishingDelay);
        }
    }

    void ReloadScene()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadScene(0);
    }
}
