
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public CarScript car;

    IEnumerator Start()
    {
        car.canMove = false;


        countdownText.color = Color.red;
        countdownText.text = "HOPE EVERYONE IS READY TO RACE!";
        yield return new WaitForSeconds(3f);

        countdownText.color = Color.red;
        countdownText.text = "DRIVERS START YOUR ENGINES!";
        yield return new WaitForSeconds(3f);

        countdownText.color = Color.red;
        countdownText.text = "READY";
        yield return new WaitForSeconds(3f);

        countdownText.color = Color.red;
        countdownText.text = "SET";
        yield return new WaitForSeconds(3f);

        countdownText.color = Color.green;
        countdownText.text = "GO!";

        car.canMove = true;

        yield return new WaitForSeconds(2f);

        countdownText.text = "";
    }
}