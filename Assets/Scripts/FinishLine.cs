
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public LapManager lapManager;
    public float minimumLapTime = 20f;

    private float lastLapTime;
    private bool raceStarted = false;

    void Start()
    {
        lastLapTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!raceStarted)
        {
            raceStarted = true;
            lastLapTime = Time.time;
            Debug.Log("Player crossed the starting line.");
            return;
        }

        if (Time.time - lastLapTime < minimumLapTime)
            return;

        lastLapTime = Time.time;
        lapManager.CompleteLap();
    }
}
