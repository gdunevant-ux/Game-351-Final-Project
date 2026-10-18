using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // reference to the lap manager
    public LapManager lapManager;

    // minimum time before another lap counts
    public float minimumLapTime = 20f;

    // stores the last completed lap time
    private float lastLapTime;

    // checks if the race has started
    private bool raceStarted = false;

    void Start()
    {
        // save the starting time
        lastLapTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        // only the player can complete laps
        if (!other.CompareTag("Player"))
            return;

        // start the race on the first crossing
        if (!raceStarted)
        {
            raceStarted = true;
            lastLapTime = Time.time;
            return;
        }

        // prevent counting laps too quickly
        if (Time.time - lastLapTime < minimumLapTime)
            return;

        // update the lap time
        lastLapTime = Time.time;

        // complete the lap
        lapManager.CompleteLap();
    }
}
