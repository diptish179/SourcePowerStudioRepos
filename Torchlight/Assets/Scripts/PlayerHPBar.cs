using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] Image foreground;
    [SerializeField] TMP_Text hpText;
    [SerializeField] Image bloodOverlay;
    [SerializeField] Image powerbar;
    [SerializeField] TMP_Text powerText;
    [SerializeField] Image ultimatebar;
    [SerializeField] TMP_Text ultimateText;
    [SerializeField] TMP_Text goldCoinsText;

    private float blinkDuration = 0.5f; // duration of each blink
    private float glowDuration = 1f; // duration of each glow cycle
    private Color originalColor; // the original color of the bloodOverlay image

    // Start is called before the first frame update
    void Start()
    {
        originalColor = bloodOverlay.color; // cache the original color
    }

    // Update is called once per frame
    void Update()
    {
        double hpRatio = player.currentHP / player.maxHP;
        foreground.transform.localScale = new Vector3((float)hpRatio, 1, 1);
        hpText.text = "HEALTH " + Math.Round(hpRatio * 100);

        // Change color of foreground Image based on HP ratio
        //bloodOverlay.color = new Color((float)(1 - hpRatio), 0, 0, (float)(1 - hpRatio));

        if (hpRatio <= 0.5 && hpRatio > 0.3) // only apply effect when health is less that 50%
        {
            float t = Time.time % glowDuration; // get time within current glow cycle
            float alpha = Mathf.Lerp(0.2f, 0.8f, t / blinkDuration); // interpolate alpha between 0.2 and 0.8 based on time
            Color glowColor = new Color(originalColor.r * (float) hpRatio, 0, 0, alpha * (float)(1-hpRatio));

            bloodOverlay.color = glowColor; // apply the new color
        }
        else if (hpRatio <= 0.3 && hpRatio > 0) // only apply effect when health is less that 30%
        {
            float t = Time.time % (blinkDuration / 2f); // get time within current glow cycle
            float alpha = Mathf.Lerp(0.2f, 0.5f, t / (blinkDuration / 2f)); // interpolate alpha between 0.2 and 0.5 based on time
            Color glowColor = new Color(originalColor.r * (float) hpRatio,0, 0, alpha * (float)(1-hpRatio));

            bloodOverlay.color = glowColor; // apply the new color
        }
        else
        {
            bloodOverlay.color = originalColor; // reset color to original when health is full
        }


        //Power bar controls
        double powerRatio = player.currentPower / player.maxPower;
        powerbar.transform.localScale = new Vector3((float)powerRatio, 1, 1);
        powerText.text = "POWER " + Math.Round(powerRatio * 100);


        //Ultimate Bar controls
        float ultimateRatio = player.currentUltimateCoin / player.maxUltimateCoin;
        ultimatebar.transform.localScale = new Vector3(ultimateRatio, 1f, 1f); // ultimate bar to be fixed
        ultimateText.text = "ULTIMATE " + player.currentUltimateCoin;

        //Gold Coins
        goldCoinsText.text = "$ " + player.goldCoins;

    }

}
