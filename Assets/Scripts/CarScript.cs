using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float turnSpeed = 80f;
    public bool canMove = false;
    public float tiltSpeed = 5f;

    [Header("Driving Sound")]
    public float loopLength = 2f;

    private AudioSource drivingAudio;
    private float soundTimer = 0f;

    void Start()
    {
        drivingAudio = GetComponent<AudioSource>();

        if (drivingAudio == null)
        {
            Debug.LogError("No AudioSource found on the Race Car!");
        }
    }

    void Update()
    {
        if (!canMove)
        {
            StopDrivingSound();
            return;
        }

        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(move) > 0.01f)
        {
            transform.Rotate(Vector3.up * turn * turnSpeed * move * Time.deltaTime);
        }

        // Driving sound
        if (Mathf.Abs(move) > 0.01f)
        {
            if (drivingAudio != null)
            {
                if (!drivingAudio.isPlaying)
                {
                    drivingAudio.time = 0f;
                    drivingAudio.Play();
                    soundTimer = 0f;
                }

                soundTimer += Time.deltaTime;

                if (soundTimer >= loopLength)
                {
                    drivingAudio.Stop();
                    drivingAudio.time = 0f;
                    drivingAudio.Play();
                    soundTimer = 0f;
                }
            }
        }
        else
        {
            StopDrivingSound();
        }

        MatchGround();
    }

    void StopDrivingSound()
    {
        if (drivingAudio != null && drivingAudio.isPlaying)
        {
            drivingAudio.Stop();
        }

        soundTimer = 0f;
    }

    void MatchGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f))
        {
            Quaternion groundRotation =
                Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                groundRotation,
                tiltSpeed * Time.deltaTime);
        }
    }
}