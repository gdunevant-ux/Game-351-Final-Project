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
        countdownText.text = "READY";
        yield return new WaitForSeconds(1f);

        countdownText.color = Color.red;
        countdownText.text = "SET";
        yield return new WaitForSeconds(1f);


        countdownText.color = Color.green;
        countdownText.text = "GO!";
        car.canMove = true;
        yield return new WaitForSeconds(1f);

        countdownText.text = "";
    }
}