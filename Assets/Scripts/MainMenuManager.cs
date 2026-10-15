using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject hudManager;
    public CarScript playerCar;
    public PlayableDirector introTimeline;

    void Start()
    {
        // Show the menu
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        // Hide the HUD
        if (hudManager != null)
            hudManager.SetActive(false);

        // Disable driving
        if (playerCar != null)
            playerCar.canMove = false;

        // Pause the game
        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        // Resume time
        Time.timeScale = 1f;

        // Hide the menu
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        // Play intro timeline
        if (introTimeline != null)
            introTimeline.Play();

        // Start background music
        else
        {
            Debug.LogError("BackgroundMusic is not assigned in MainMenuManager!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}