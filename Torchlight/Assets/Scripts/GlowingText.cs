using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlowingText : MonoBehaviour
{
    public TMP_Text text;
    public float speed = 2f;
    public float intensity = 0.5f;
    private float timer;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime * speed;
        float alpha = Mathf.Sin(timer) * intensity + intensity;
        Color newColor = Color.Lerp(Color.white, Color.yellow, alpha);
        text.color = newColor;
    }
}
