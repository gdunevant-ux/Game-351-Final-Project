using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    // countdown text on the screen
    public TextMeshProUGUI countdownText;

    // player's car
    public CarScript car;

    IEnumerator Start()
    {
        //stop the car from moving
        car.canMove = false;

        //wait before starting the countdown
        countdownText.color = Color.red;
        countdownText.text = " ";
        yield return new WaitForSeconds(12f);

        //show ready
        countdownText.color = Color.red;
        countdownText.text = "READY";
        yield return new WaitForSeconds(3f);

        //show set
        countdownText.color = Color.red;
        countdownText.text = "SET";
        yield return new WaitForSeconds(3f);

        //show go
        countdownText.color = Color.green;
        countdownText.text = "GO!";

        //allow the player to drive
        car.canMove = true;

        //keep the go text on screen
        yield return new WaitForSeconds(2f);

        //hide the countdown text
        countdownText.text = "";
    }
}