using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    // reference to the player's car
    public CarScript car;

    // displays the player's speed
    public TextMeshProUGUI speedText;

    // displays the race timer
    public TextMeshProUGUI timerText;

    // keeps track of race time
    private float raceTime = 0f;

    // delay before showing the hud
    private float startDelay = 6.5f;

    // checks if the hud is visible
    private bool hudShown = false;

    void Start()
    {
        // hide all hud elements
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // wait before showing the hud
        if (!hudShown)
        {
            startDelay -= Time.deltaTime;

            if (startDelay <= 0f)
            {
                hudShown = true;

                // show all hud elements
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        // calculate the player's speed
        float speed = Input.GetAxis("Vertical") * car.moveSpeed;
        speedText.text = "Speed: " + Mathf.Abs(speed).ToString("0") + " MPH";

        // update the race timer
        if (hudShown)
        {
            raceTime += Time.deltaTime;
        }

        // convert time into minutes, seconds, and hundredths
        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        int hundredths = Mathf.FloorToInt((raceTime * 100) % 100);

        // display the formatted timer
        timerText.text = string.Format("{0:00}:{1:00}.{2:00}",
            minutes, seconds, hundredths);
    }
}