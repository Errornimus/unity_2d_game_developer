using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float crashDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSound;

    bool HasCrashed { get; set; } = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle" && !HasCrashed)
        {
            HasCrashed = true;
            FindFirstObjectByType<PlayerController>().DisableControls();

            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSound);

            Invoke("ReloadScene", crashDelay);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
