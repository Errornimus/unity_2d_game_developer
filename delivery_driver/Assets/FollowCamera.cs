using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject car;

    // the cameras position should be the same as the cars position

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = car.transform.position + new Vector3(0, 0, -10f);
    }
}
