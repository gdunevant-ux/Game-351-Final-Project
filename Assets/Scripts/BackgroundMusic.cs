using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public float loopLength = 43f;

    private AudioSource musicSource;
    private float timer = 0f;
    private bool started = false;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        if (musicSource == null)
        {
            Debug.LogError("No AudioSource found!");
            return;
        }

        musicSource.playOnAwake = false;
    }

    void Update()
    {
        if (musicSource == null)
            return;

        // Start music once the game is unpaused
        if (!started && Time.timeScale > 0f)
        {
            musicSource.Play();
            started = true;
            timer = 0f;
        }

        if (!started)
            return;

        timer += Time.deltaTime;

        if (timer >= loopLength)
        {
            musicSource.Stop();
            musicSource.time = 0f;
            musicSource.Play();
            timer = 0f;
        }
    }
}