using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearCameraFollow : MonoBehaviour
{
    //for the player car
    public Transform car;

    void LateUpdate()
    {
        // rotates the cam to face behind the car
        transform.rotation = Quaternion.Euler(
            car.eulerAngles.x,
            car.eulerAngles.y + 180f,
            car.eulerAngles.z
        );
    }
}