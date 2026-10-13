using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarScript : MonoBehaviour
{
    [Header("Ground Driving")]
    public float moveSpeed = 15f;
    public float turnSpeed = 80f;
    public float tiltSpeed = 5f;
    public float steeringSmoothSpeed = 5f;

    [Range(0f, 0.5f)]
    public float turnSpeedReduction = 0.1f;

    [Header("Boost")]
    public float boostFadeRate = 1f;

    [Header("Midair Control")]
    public float airTurnSpeed = 45f;
    public float airControl = 2f;

    private Rigidbody body;

    private float moveInput;
    private float turnInput;
    private float targetTurnInput;
    private bool braking;

    private float boostEndTime;
    private float boostMultiplier = 1f;

    private bool grounded;
    private Vector3 groundNormal = Vector3.up;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        targetTurnInput = Input.GetAxisRaw("Horizontal");
        braking = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        turnInput = Mathf.MoveTowards(
            turnInput,
            targetTurnInput,
            Mathf.Max(0.01f, steeringSmoothSpeed) * dt);

        if (Time.time >= boostEndTime)
        {
            boostMultiplier = Mathf.MoveTowards(
                boostMultiplier,
                1f,
                Mathf.Max(0.01f, boostFadeRate) * dt);
        }

        bool touchingGround = grounded;
        grounded = false;

        if (touchingGround)
            HandleGroundMovement(dt);
        else
            HandleAirMovement(dt);
    }

    void HandleGroundMovement(float dt)
    {
        Quaternion aligned =
            Quaternion.FromToRotation(
                body.rotation * Vector3.up,
                groundNormal) * body.rotation;

        Quaternion nextRotation = Quaternion.Slerp(
            body.rotation,
            aligned,
            tiltSpeed * dt);

        float steeringDirection =
            moveInput < -0.01f ? -1f : 1f;

        nextRotation =
            Quaternion.AngleAxis(
                turnInput * turnSpeed * steeringDirection * dt,
                groundNormal) * nextRotation;

        body.angularVelocity = Vector3.zero;
        body.MoveRotation(nextRotation);

        Vector3 forward = Vector3.ProjectOnPlane(
            nextRotation * Vector3.forward,
            groundNormal).normalized;

        // Small speed reduction that follows smoothed steering.
        float cornerMultiplier =
            1f - Mathf.Clamp(turnSpeedReduction, 0f, 0.5f)
            * Mathf.Abs(turnInput);

        float targetSpeed = braking
            ? 0f
            : moveInput * moveSpeed
            * boostMultiplier * cornerMultiplier;

        Vector3 currentGroundVelocity =
            Vector3.ProjectOnPlane(body.velocity, groundNormal);

        Vector3 desiredGroundVelocity = forward * targetSpeed;

        // Preserve velocity perpendicular to the road.
        body.AddForce(
            desiredGroundVelocity - currentGroundVelocity,
            ForceMode.VelocityChange);
    }

    void HandleAirMovement(float dt)
    {
        float steeringDirection =
            moveInput < -0.01f ? -1f : 1f;

        Quaternion airRotation =
            Quaternion.AngleAxis(
                turnInput * airTurnSpeed * steeringDirection * dt,
                Vector3.up) * body.rotation;

        body.MoveRotation(airRotation);

        Vector3 horizontalVelocity =
            Vector3.ProjectOnPlane(body.velocity, Vector3.up);

        Vector3 airForward = Vector3.ProjectOnPlane(
            airRotation * Vector3.forward,
            Vector3.up).normalized;

        if (airForward.sqrMagnitude < 0.01f)
            return;

        float travelDirection =
            Vector3.Dot(horizontalVelocity, airForward) < 0f
            ? -1f
            : 1f;

        Vector3 targetVelocity =
            airForward * horizontalVelocity.magnitude * travelDirection;

        float controlAmount =
            1f - Mathf.Exp(-Mathf.Max(0f, airControl) * dt);

        // Leave vertical velocity unchanged.
        body.AddForce(
            (targetVelocity - horizontalVelocity) * controlAmount,
            ForceMode.VelocityChange);
    }

    public void ActivateBoost(float multiplier, float duration)
    {
        boostMultiplier = Mathf.Max(1f, multiplier);
        boostEndTime = Time.time + Mathf.Max(0f, duration);
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        CheckGround(collision);
    }

    void CheckGround(Collision collision)
    {
        Vector3 normalSum = Vector3.zero;
        int groundContacts = 0;

        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;

            if (Vector3.Dot(normal, Vector3.up) > 0.4f)
            {
                normalSum += normal;
                groundContacts++;
            }
        }

        if (groundContacts > 0)
        {
            groundNormal = normalSum.normalized;
            grounded = true;
        }
    }
}