using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    // displays the current lap
    public TextMeshProUGUI lapText;

    // total number of laps
    public int totalLaps = 2;

    // reference to the race results ui
    public RaceResultsUI raceResultsUI;

    // keeps track of the current lap
    private int currentLap = 0;

    // checks if the race is finished
    private bool raceFinished = false;

    void Start()
    {
        // show the starting lap
        lapText.text = "Lap: " + currentLap + " / " + totalLaps;
    }

    public void CompleteLap()
    {
        // stop if the race is already finished
        if (raceFinished)
            return;

        // increase the lap count
        currentLap++;

        // check if the player finished the race
        if (currentLap >= totalLaps)
        {
            raceFinished = true;
            lapText.text = "FINISHED!";

            Debug.Log("Player finished the race!");

            // show the victory screen
            if (raceResultsUI != null)
            {
                raceResultsUI.ShowVictory();
            }
        }
        else
        {
            // update the lap display
            lapText.text = "Lap: " + currentLap + " / " + totalLaps;
        }
    }
}