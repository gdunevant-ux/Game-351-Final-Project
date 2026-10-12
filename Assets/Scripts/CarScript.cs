using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float turnSpeed = 80f;

    public bool canMove = false;
    public float tiltSpeed = 5f;

    void Update()
    {
        if (!canMove)
        {
            return;
        }
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

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