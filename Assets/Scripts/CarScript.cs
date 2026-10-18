using UnityEngine;

public class CarScript : MonoBehaviour
{
    // car movement speed
    public float moveSpeed = 15f;
    public float turnSpeed = 80f;

    // allows the car to move
    public bool canMove = false;

    // controls how fast the car matches the ground
    public float tiltSpeed = 5f;

    // engine sound
    private AudioSource engineAudio;

    void Awake()
    {
        // get the audio source on the car
        engineAudio = GetComponent<AudioSource>();

        if (engineAudio != null)
        {
            engineAudio.playOnAwake = false;
            engineAudio.loop = true;
            engineAudio.Stop();
        }
    }

    void Update()
    {
        // stop if the race has not started
        if (!canMove)
        {
            if (engineAudio != null && engineAudio.isPlaying)
            {
                engineAudio.Stop();
            }
            return;
        }

        // get player input
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // play the engine sound while driving
        if (engineAudio != null)
        {
            bool isMoving =
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D);

            if (isMoving)
            {
                if (!engineAudio.isPlaying)
                {
                    engineAudio.Play();
                }
            }
            else
            {
                if (engineAudio.isPlaying)
                {
                    engineAudio.Stop();
                }
            }
        }

        // move the car
        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);

        // turn the car while moving
        if (Mathf.Abs(move) > 0.01f)
        {
            transform.Rotate(Vector3.up * turn * turnSpeed * move * Time.deltaTime);
        }

        // match the car to the ground
        MatchGround();
    }

    void MatchGround()
    {
        RaycastHit hit;

        // check for the ground below the car
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f))
        {
            Quaternion groundRotation =
                Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            // smoothly rotate to match the ground
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                groundRotation,
                tiltSpeed * Time.deltaTime);
        }
    }
}