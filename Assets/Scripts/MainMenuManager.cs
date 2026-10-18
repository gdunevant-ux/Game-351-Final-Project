using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class MainMenuManager : MonoBehaviour
{
    // main menu ui
    public GameObject mainMenuPanel;

    // hud shown during gameplay
    public GameObject hudManager;

    // player's car
    public CarScript playerCar;

    // intro cutscene timeline
    public PlayableDirector introTimeline;

    // intro camera
    public CinemachineVirtualCamera trackCam;

    // gameplay camera
    public CinemachineVirtualCamera thirdPersonCam;

    void Start()
    {
        // show the main menu
        mainMenuPanel.SetActive(true);

        // hide the hud
        hudManager.SetActive(false);

        // disable player movement
        playerCar.enabled = false;

        // pause the game
        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        // resume the game
        Time.timeScale = 1f;

        // hide the main menu
        mainMenuPanel.SetActive(false);

        // switch to the intro camera
        trackCam.Priority = 20;
        thirdPersonCam.Priority = 10;

        // play the intro timeline
        introTimeline.Play();

        // wait for the intro to finish
        StartCoroutine(WaitForIntro());
    }

    IEnumerator WaitForIntro()
    {
        // wait until the timeline ends
        yield return new WaitForSeconds((float)introTimeline.duration);

        // switch back to the gameplay camera
        trackCam.Priority = 10;
        thirdPersonCam.Priority = 20;

        // enable the player's car
        playerCar.enabled = true;

        // show the hud
        hudManager.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // stop play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // close the game
        Application.Quit();
#endif
    }
}