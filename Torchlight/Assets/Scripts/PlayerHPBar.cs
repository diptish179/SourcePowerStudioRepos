using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{

    [SerializeField] PlayerController player;
    public GameObject enemy;
    [SerializeField] Image foreground;
    [SerializeField] TMP_Text hpText;
    [SerializeField] Image bloodOverlay;
    [SerializeField] Image powerbar;
    [SerializeField] TMP_Text powerText;
    [SerializeField] Image ultimatebar;
    [SerializeField] TMP_Text ultimateText;
    [SerializeField] TMP_Text goldCoinsText;
    [SerializeField] Image deathSkull;
    [SerializeField] TMP_Text deathCountText;

    private float blinkDuration = 0.5f; // duration of each blink
    private float glowDuration = 1f; // duration of each glow cycle
    private Color originalColor; // the original color of the bloodOverlay image
    private Color originalUltimateTextsColor;
    private Color originalhpTextColor;
    

    // Start is called before the first frame update
    void Start()
    {
        originalColor = bloodOverlay.color; // cache the original color
        originalUltimateTextsColor = ultimateText.color;
        originalhpTextColor = hpText.color;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        // At the start of the game
        TitleManager.saveData.killCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
            double hpRatio = player.currentHP / player.maxHP;
            foreground.transform.localScale = new Vector3((float)hpRatio, 1, 1);
            hpText.text = "HEALTH " + Math.Round(hpRatio * 100);

            if (hpRatio <= 0.5 && hpRatio > 0.3) // only apply effect when health is less that 50%
            {
                float t = Time.time % glowDuration; // get time within current glow cycle
                float alpha = Mathf.Lerp(0.2f, 0.8f, t / blinkDuration); // interpolate alpha between 0.2 and 0.8 based on time
                Color glowColor = new Color(originalColor.r * (float)hpRatio, 0, 0, alpha * (float)(1 - hpRatio));
                hpText.color = originalhpTextColor;
                bloodOverlay.color = glowColor; // apply the new color
            }
            else if (hpRatio <= 0.3 && hpRatio > 0) // only apply effect when health is less that 30%
            {
                float t = Time.time % (blinkDuration / 2f); // get time within current glow cycle
                float alpha = Mathf.Lerp(0.2f, 0.5f, t / (blinkDuration / 2f)); // interpolate alpha between 0.2 and 0.5 based on time
                Color glowColor = new Color(originalColor.r * (float)hpRatio, 0, 0, alpha * (float)(1 - hpRatio));
                hpText.color = originalhpTextColor;
                bloodOverlay.color = glowColor; // apply the new color
            }
            else
            {
                bloodOverlay.color = originalColor; // reset color to original when health is full
            }

            if (hpRatio <= 0)
            {
                hpText.text = "LOW HP";
                float t1 = Time.time % (blinkDuration * 2); // get time within current blink cycle
                float alpha1 = Mathf.Lerp(0.2f, 1f, t1 / blinkDuration); // interpolate alpha between 0.2 and 1 based on time
                Color flashColor = new Color(1f, 0, 0, alpha1);
                hpText.color = flashColor;
                float t = Time.time % (blinkDuration / 4f); // get time within current glow cycle
                float alpha = Mathf.Lerp(0.2f, 1f, t / (blinkDuration / 4f)); // interpolate alpha between 0.2 and 1 based on time
                Color glowColor = new Color(originalColor.r * 0.2f, 0, 0, alpha);
                
                bloodOverlay.color = glowColor; // apply the new color
            }
        



        //Power bar controls
        double powerRatio = player.currentPower / player.maxPower;
        powerbar.transform.localScale = new Vector3((float)powerRatio, 1, 1);
        powerText.text = "POWER " + Math.Round(powerRatio * 100);
        if(powerRatio <= 0)
        {
            powerText.text = "NO POWER";
            float t1 = Time.time % (blinkDuration * 2); // get time within current blink cycle
            float alpha1 = Mathf.Lerp(0.2f, 1f, t1 / blinkDuration); // interpolate alpha between 0.2 and 1 based on time
            Color flashColor = new Color(0, 0, 1f, alpha1);
            powerText.color = flashColor;
        }
        else
        {
            powerText.color = originalUltimateTextsColor;
        }


        //Ultimate Bar controls
        float ultimateRatio = player.currentUltimateCoin / player.maxUltimateCoin;
        ultimatebar.transform.localScale = new Vector3(ultimateRatio, 1f, 1f); // ultimate bar to be fixed
        if (player.currentUltimateCoin == player.maxUltimateCoin)
        {
            ultimateText.text = "ULTI READY";
            // Flash the text green
            float t = Time.time % (blinkDuration * 2); // get time within current blink cycle
            float alpha = Mathf.Lerp(0.2f, 1f, t / blinkDuration); // interpolate alpha between 0.2 and 1 based on time
            Color flashColor = new Color(0, 1f, 0, alpha);
            ultimateText.color = flashColor;
        }
        else
        {
            ultimateText.text = "ULTIMATE " + player.currentUltimateCoin;
            // Reset the text color to original state
            ultimateText.color = originalUltimateTextsColor;
        }


        //Gold Coins
        goldCoinsText.text = "$ " + player.goldCoins;

        //Kill count
        deathCountText.text = "Kills " + TitleManager.saveData.killCount.ToString();


    }

}
