using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    public TextMeshProUGUI lapText;

    public int totalLaps = 2;

    private int currentLap = 0;

    void Start()
    {
        lapText.text = "Lap: " + currentLap + " / " + totalLaps;
    }

    public void CompleteLap()
    {
        if (currentLap < totalLaps)
        {
            currentLap++;

            lapText.text = "Lap: " + currentLap + " / " + totalLaps;
        }
        else
        {
            lapText.text = "FINISHED!";
        }
    }
}