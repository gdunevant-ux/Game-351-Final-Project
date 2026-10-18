using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    //speed while the boost is active
    public float boostSpeed = 30f;

    //how long the boost lasts
    public float boostTime = 2f;

    //sound played when the pad is used
    public AudioSource boostSound;

    private void OnTriggerEnter(Collider other)
    {
        //check if the object is the player's car
        CarScript car = other.GetComponent<CarScript>();

        if (car != null)
        {
            //start the speed boost
            StartCoroutine(Boost(car));

            //play the boost sound
            if (boostSound != null)
            {
                boostSound.Play();
            }
        }
    }

    IEnumerator Boost(CarScript car)
    {
        //save the original speed
        float originalSpeed = car.moveSpeed;

        //change the boost speed
        car.moveSpeed = boostSpeed;

        //wait for the boost to finish
        yield return new WaitForSeconds(boostTime);

        //restore the original speed
        car.moveSpeed = originalSpeed;
    }
}
