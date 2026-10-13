
using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    public TextMeshProUGUI lapText;
    public int totalLaps = 2;
    public RaceResultsUI raceResultsUI;

    private int currentLap = 0;
    private bool raceFinished = false;

    void Start()
    {
        lapText.text = "Lap: " + currentLap + " / " + totalLaps;
    }

    public void CompleteLap()
    {
        if (raceFinished)
            return;

        currentLap++;

        if (currentLap >= totalLaps)
        {
            raceFinished = true;
            lapText.text = "FINISHED!";

            Debug.Log("Player finished the race!");

            if (raceResultsUI != null)
            {
                raceResultsUI.ShowVictory();
            }
        }
        else
        {
            lapText.text = "Lap: " + currentLap + " / " + totalLaps;
        }
    }
}
