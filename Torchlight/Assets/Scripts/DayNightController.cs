using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public float totalTime = 300f; // Total time for a full day-night cycle (in seconds)
    public Color dayColor = new Color(1f, 0.9f, 0.5f); // Color for daytime
    public Color nightColor = new Color(0.05f, 0.05f, 0.15f); // Color for nighttime
    public float transitionTime = 30f; // Time for the transition between night and day (in seconds)

    private Camera mainCamera;
    private float startTime;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate the current time of day based on the elapsed time since the script started
        float elapsedTime = Time.time - startTime;
        float timeOfDay = (elapsedTime % totalTime) / totalTime;

        // Calculate the remaining time until the next day starts
        float remainingTime = (totalTime - elapsedTime % totalTime) % totalTime;

        // Interpolate between the day and night colors based on the current time of day
        Color currentColor;
        if (timeOfDay < 0.5f)
        {
            currentColor = Color.Lerp(dayColor, nightColor, timeOfDay * 2f);
        }
        else
        {
            currentColor = Color.Lerp(nightColor, dayColor, (timeOfDay - 0.5f) * 2f);
        }

        // Apply a transition effect between night and day
        if (remainingTime < transitionTime)
        {
            float transitionFactor = 1f - remainingTime / transitionTime;
            Color transitionColor = Color.Lerp(nightColor, dayColor, transitionFactor);
            currentColor = Color.Lerp(currentColor, transitionColor, 1f - remainingTime / transitionTime);
        }

        // Set the camera's skybox color to the current color
        mainCamera.backgroundColor = currentColor;
    }
}

