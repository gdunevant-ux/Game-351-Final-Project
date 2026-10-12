using UnityEngine;

public class RearCameraFollow : MonoBehaviour
{
    public Transform car;

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(
            car.eulerAngles.x,
            car.eulerAngles.y + 180f,
            car.eulerAngles.z
        );
    }
}