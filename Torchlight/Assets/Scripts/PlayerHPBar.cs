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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        double hpRatio = player.currentHP / player.maxHP;
        foreground.transform.localScale = new Vector3((float)hpRatio, 1, 1);
        hpText.text = "HEALTH " + Math.Round(hpRatio * 100);

        // Change color of foreground Image based on HP ratio
        bloodOverlay.color = new Color((float)(1 - hpRatio), 0, 0, (float)(1 - hpRatio) );

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
