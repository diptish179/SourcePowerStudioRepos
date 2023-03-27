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
    }




}
