using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorEffect : MonoBehaviour
{
    private Image image;
    private float colorChangeTime = 1f; // Time between color changes in seconds
    private float timer = 0f; // Timer to keep track of time

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= colorChangeTime) // If it's time to change the color
        {
            timer = 0f; // Reset the timer
            image.color = new Color(Random.value, Random.value, Random.value); // Set a random RGB value for the image color
        }
    }
}