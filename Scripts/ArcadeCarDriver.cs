using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCarDriver : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float reverseSpeed = 7f;
    public float acceleration = 12f;
    public float braking = 25f;
    public float turnSpeed = 80f;
    public float grip = 10f;
    public float groundAlignSpeed = 8f;

    public Transform groundProbe;
    public float groundCheckDistance = 0.3f;

    Rigidbody body;
    float throttle;
    float steering;
    bool brakeHeld;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steering = Input.GetAxis("Horizontal");
        brakeHeld = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (groundProbe == null)
            return;

        if (!FindGround(out RaycastHit ground))
            return; // In the air: preserve momentum and let gravity act.

        float dt = Time.fixedDeltaTime;
        Vector3 normal = ground.normal;

        // Align with the road, then apply responsive steering.
        Quaternion aligned = Quaternion.FromToRotation(
            transform.up, normal) * body.rotation;

        Quaternion rotation = Quaternion.Slerp(
            body.rotation, aligned, groundAlignSpeed * dt);

        float direction = throttle < -0.01f ? -1f : 1f;

        rotation = Quaternion.AngleAxis(
            steering * turnSpeed * direction * dt, normal) * rotation;

        body.MoveRotation(rotation);

        Vector3 forward = Vector3.ProjectOnPlane(
            rotation * Vector3.forward, normal).normalized;

        Vector3 right = Vector3.Cross(normal, forward).normalized;

        float currentSpeed = Vector3.Dot(body.velocity, forward);
        float targetSpeed = brakeHeld ? 0f :
            throttle * (throttle >= 0f ? forwardSpeed : reverseSpeed);

        bool slowingDown = brakeHeld ||
            Mathf.Abs(throttle) < 0.01f ||
            currentSpeed * throttle < 0f;

        float rate = slowingDown ? braking : acceleration;

        float nextSpeed = Mathf.MoveTowards(
            currentSpeed, targetSpeed, rate * dt);

        // Change forward speed without removing vertical momentum.
        body.AddForce(
            forward * (nextSpeed - currentSpeed),
            ForceMode.VelocityChange);

        // Remove sideways sliding gradually.
        float sidewaysSpeed = Vector3.Dot(body.velocity, right);
        float gripAmount = 1f - Mathf.Exp(-grip * dt);

        body.AddForce(
            -right * sidewaysSpeed * gripAmount,
            ForceMode.VelocityChange);
    }

    bool FindGround(out RaycastHit closest)
    {
        closest = default;
        float nearest = float.PositiveInfinity;

        RaycastHit[] hits = Physics.RaycastAll(
            groundProbe.position,
            -transform.up,
            groundCheckDistance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore);

        foreach (RaycastHit hit in hits)
        {
            // Ignore this car's own colliders and steep walls.
            if (hit.collider.attachedRigidbody == body ||
                Vector3.Dot(hit.normal, Vector3.up) < 0.3f)
                continue;

            if (hit.distance < nearest)
            {
                nearest = hit.distance;
                closest = hit;
            }
        }

        return nearest < float.PositiveInfinity;
    }
}