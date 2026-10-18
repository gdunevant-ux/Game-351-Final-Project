using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceResultsUI : MonoBehaviour
{
    //victory screen
    public GameObject victoryPanel;

    //defeat screen
    public GameObject defeatPanel;

    //checks if the race result has been shown
    private bool resultDecided = false;

    void Start()
    {
        //reset the race result
        resultDecided = false;

        //hide both result screens
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    [ContextMenu("Test Victory")]
    public void ShowVictory()
    {
        //stop if a result is already shown
        if (resultDecided)
            return;

        //mark the race as finished
        resultDecided = true;

        //show the victory screen
        victoryPanel.SetActive(true);
        defeatPanel.SetActive(false);
    }

    [ContextMenu("Test Defeat")]
    public void ShowDefeat()
    {
        //stop if a result is already shown
        if (resultDecided)
            return;

        //mark the race as finished
        resultDecided = true;

        //show the defeat screen
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(true);
    }
}