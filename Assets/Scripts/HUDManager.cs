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

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        StartCoroutine(ShowHUD());
    }

    void Update()
    {
        float speed = Input.GetAxis("Vertical") * car.moveSpeed;
        speedText.text = "Speed: " + Mathf.Abs(speed).ToString("0") + " MPH";

        if (startDelay > 0f)
        {
            startDelay -= Time.deltaTime;
        }
        else
        {
            raceTime += Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        int hundredths = Mathf.FloorToInt((raceTime * 100) % 100);

        timerText.text = string.Format("{0:00}:{1:00}.{2:00}",
            minutes, seconds, hundredths);
    }

    IEnumerator ShowHUD()
    {
        yield return new WaitForSeconds(startDelay);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}