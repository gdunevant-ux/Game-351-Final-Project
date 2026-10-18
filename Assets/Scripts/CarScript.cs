using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float turnSpeed = 80f;

    public bool canMove = false;
    public float tiltSpeed = 5f;

    private AudioSource engineAudio;

    void Awake()
    {
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
        if (!canMove)
        {
            if (engineAudio != null && engineAudio.isPlaying)
            {
                engineAudio.Stop();
            }
            return;
        }

        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");


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


        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);


        if (Mathf.Abs(move) > 0.01f)
        {
            transform.Rotate(Vector3.up * turn * turnSpeed * move * Time.deltaTime);
        }

        MatchGround();
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