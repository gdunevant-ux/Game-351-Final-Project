
using UnityEngine;

public class RaceResultsUI : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    private bool resultDecided = false;

    void Start()
    {
        resultDecided = false;

        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    [ContextMenu("Test Victory")]
    public void ShowVictory()
    {
        if (resultDecided)
            return;

        resultDecided = true;

        victoryPanel.SetActive(true);
        defeatPanel.SetActive(false);

        Debug.Log("Player won the race!");
    }

    [ContextMenu("Test Defeat")]
    public void ShowDefeat()
    {
        if (resultDecided)
            return;

        resultDecided = true;

        victoryPanel.SetActive(false);
        defeatPanel.SetActive(true);

        Debug.Log("AI won the race! Player lost.");
    }
}
