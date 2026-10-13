
using System.Collections;
using UnityEngine;

public class RaceIntroCamera : MonoBehaviour
{
    public GameObject introCamera;
    public GameObject gameplayCamera;
    public float introDuration = 5f;

    private IEnumerator Start()
    {
        if (introCamera == null || gameplayCamera == null)
        {
            Debug.LogError("Assign both cameras in the Inspector.");
            yield break;
        }

        Camera intro = introCamera.GetComponent<Camera>();
        Camera gameplay = gameplayCamera.GetComponent<Camera>();

        if (intro == null || gameplay == null)
        {
            Debug.LogError("Both objects need Camera components.");
            yield break;
        }

        introCamera.SetActive(true);
        gameplay.enabled = false;
        intro.enabled = true;

        yield return new WaitForSeconds(introDuration);

        intro.enabled = false;
        gameplay.enabled = true;
        introCamera.SetActive(false);
    }
}
