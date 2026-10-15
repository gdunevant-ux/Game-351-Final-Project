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

    public CinemachineVirtualCamera trackCam;
    public CinemachineVirtualCamera thirdPersonCam;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        hudManager.SetActive(false);

        playerCar.enabled = false;

        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;

        mainMenuPanel.SetActive(false);

        // Intro camera becomes active
        trackCam.Priority = 20;
        thirdPersonCam.Priority = 10;

        introTimeline.Play();

        StartCoroutine(WaitForIntro());
    }

    IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds((float)introTimeline.duration);

        // Switch back to gameplay camera
        trackCam.Priority = 10;
        thirdPersonCam.Priority = 20;

        playerCar.enabled = true;

        hudManager.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}