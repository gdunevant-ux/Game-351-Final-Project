using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public CarScript car;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI timerText;

    private float raceTime = 0f;
    private float startDelay = 6.5f;
    private bool hudShown = false;

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!hudShown)
        {
            startDelay -= Time.deltaTime;

            if (startDelay <= 0f)
            {
                hudShown = true;

                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        float speed = Input.GetAxis("Vertical") * car.moveSpeed;
        speedText.text = "Speed: " + Mathf.Abs(speed).ToString("0") + " MPH";

        if (hudShown)
        {
            raceTime += Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        int hundredths = Mathf.FloorToInt((raceTime * 100) % 100);

        timerText.text = string.Format("{0:00}:{1:00}.{2:00}",
            minutes, seconds, hundredths);
    }
}