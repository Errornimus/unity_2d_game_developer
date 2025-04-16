using Unity.VisualScripting;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustTrailEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
            dustTrailEffect.Play();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
            dustTrailEffect.Stop();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
