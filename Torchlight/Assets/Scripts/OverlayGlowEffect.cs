using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayGlowEffect : MonoBehaviour
{
  
    public float maxGlowIntensity = 1f;
    public float minGlowIntensity = 0f;
    public float blinkSpeed = 1f;
    public Color glowColor = Color.red;

    private float targetGlowIntensity;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.shader = Shader.Find("Unlit/Transparent");
        targetGlowIntensity = maxGlowIntensity;
    }

    void Update()
    {
        float currentGlowIntensity = Mathf.Lerp(material.GetColor("_EmissionColor").maxColorComponent, targetGlowIntensity, Time.deltaTime * blinkSpeed);
        material.SetColor("_EmissionColor", glowColor * currentGlowIntensity);

        if (Mathf.Abs(currentGlowIntensity - targetGlowIntensity) < 0.01f)
        {
            targetGlowIntensity = (targetGlowIntensity == maxGlowIntensity) ? minGlowIntensity : maxGlowIntensity;
        }
    }
}

