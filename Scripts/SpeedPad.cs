using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    [Header("Boost")]
    public float multiplier = 1.5f;
    public float duration = 2f;

    [Header("Audio")]
    public AudioSource boostAudio;
    public AudioClip boostClip;

    [Range(0f, 1f)]
    public float boostVolume = 0.8f;

    private void OnTriggerEnter(Collider other)
    {
        CarScript car = other.GetComponentInParent<CarScript>();

        if (car == null || !car.isActiveAndEnabled)
            return;

        car.ActivateBoost(multiplier, duration);

        if (boostAudio != null && boostClip != null)
        {
            boostAudio.PlayOneShot(boostClip, boostVolume);
        }
    }
}