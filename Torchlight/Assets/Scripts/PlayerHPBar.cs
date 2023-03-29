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
        bloodOverlay.color = new Color((float)(1 - hpRatio), 0, 0, (float)(1 - hpRatio));

        double powerRatio = player.currentPower / player.maxPower;
        powerbar.transform.localScale = new Vector3((float)powerRatio, 1, 1);
        powerText.text = "POWER " + Math.Round(powerRatio * 100);

    }




}
