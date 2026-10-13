using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarReset : MonoBehaviour
{
    public Transform resetPoint;

    private Rigidbody body;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool resetRequested;

    void Awake()
    {
        body = GetComponent<Rigidbody>();

        startingPosition = body.position;
        startingRotation = body.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (!resetRequested)
            return;

        resetRequested = false;

        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        body.position = resetPoint != null
            ? resetPoint.position
            : startingPosition;

        body.rotation = resetPoint != null
            ? resetPoint.rotation
            : startingRotation;
    }
}