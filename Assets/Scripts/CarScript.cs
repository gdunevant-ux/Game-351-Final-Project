using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float turnSpeed = 80f;

    void Start()
    {

    }

    void Update()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");


        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(move) > 0.01f)
        {
            transform.Rotate(Vector3.up * turn * turnSpeed * move * Time.deltaTime);
        }
    }
}
