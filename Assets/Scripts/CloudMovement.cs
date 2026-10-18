using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    //how far the cloud moves
    public float moveDistance = 5f;

    //how fast the cloud moves
    public float moveSpeed = 0.5f;

    //starting position of the cloud
    private Vector3 startPosition;

    void Start()
    {
        //save the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        //move the cloud back and forth
        float x = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        //update the cloud position
        transform.position = startPosition + new Vector3(x, 0f, 0f);
    }
}