using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public string[] messages =
    {
        "Welcome to the track!",
        "Keep your speed up!",
        "Watch those corners!",
        "Looking good!",
        "Don't hit the barriers!",
        "Stay focused!",
        "Full throttle!",
        "Enjoy the drive!",
        "You got this!",
        "Smooth driving wins races!",
        "Watch out!",
        "Stay focused, you gt this kid",
        "I beleive in you champ",
        "Don't blink",
        "Think ahead",
        "Don't crash our car kid",
        "Drive like you mean it!",
        "Can you beat your best time?",
        "Keep those tires on the road!"
    };

    public float messageDuration = 4f;
    public float timeBetweenMessages = 12f;

    private float timer = 0f;
    private int lastMessage = -1;

    void Start()
    {
        messageText.text = "";
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenMessages)
        {
            ShowRandomMessage();
            timer = 0f;
        }
    }

    void ShowRandomMessage()
    {
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, messages.Length);
        }
        while (randomIndex == lastMessage);

        lastMessage = randomIndex;

        messageText.text = messages[randomIndex];

        CancelInvoke(nameof(ClearMessage));
        Invoke(nameof(ClearMessage), messageDuration);
    }

    void ClearMessage()
    {
        messageText.text = "";
    }
}