using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomMessage : MonoBehaviour
{
    //text used to show messages
    public TextMeshProUGUI messageText;
    //lists of random messages
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

    //duration od bessage and time between
    public float messageDuration = 4f;
    public float timeBetweenMessages = 12f;

    //tracks the messages time, and also stores last message
    private float timer = 0f;
    private int lastMessage = -1;

    void Start()
    {
        messageText.text = "";
    }

    void Update()
    {
        //update the timer
        timer += Time.deltaTime;

        //show message when timer ends
        if (timer >= timeBetweenMessages)
        {
            ShowRandomMessage();
            timer = 0f;
        }
    }

    void ShowRandomMessage()
    {
        int randomIndex;

        //insure no repeats with messages until all are played
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