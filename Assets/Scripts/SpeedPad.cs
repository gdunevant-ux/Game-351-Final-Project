using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float boostSpeed = 30f;
    public float boostTime = 2f;

    public AudioSource boostSound;

    private void OnTriggerEnter(Collider other)
    {
        CarScript car = other.GetComponent<CarScript>();

        if (car != null)
        {
            StartCoroutine(Boost(car));

            if (boostSound != null)
            {
                boostSound.Play();
            }
        }
    }

    IEnumerator Boost(CarScript car)
    {
        float originalSpeed = car.moveSpeed;

        car.moveSpeed = boostSpeed;

        yield return new WaitForSeconds(boostTime);

        car.moveSpeed = originalSpeed;
    }
}
