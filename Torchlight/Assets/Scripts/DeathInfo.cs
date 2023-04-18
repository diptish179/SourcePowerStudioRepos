using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathInfo : MonoBehaviour
{
    //[SerializeField] Player player;
    //[SerializeField] Enemy enemy;
    //[SerializeField] Image foreground;
    [SerializeField] TMP_Text totalKills;
    [SerializeField] TMP_Text totalCoins;
    [SerializeField] TMP_Text UltiCount;
    [SerializeField] TMP_Text TimeSurvivedText;
    //[SerializeField] TMP_Text totalCrystals;
    //[SerializeField] TMP_Text totalHealPotions;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totalKills.text = "Total Kills: " + TitleManager.saveData.killCount.ToString();
        totalCoins.text = "Gold Coins: " + TitleManager.saveData.goldCoins.ToString();
        UltiCount.text = "Ultimate Used: " + TitleManager.saveData.ultimateUsedCount.ToString();
        TimeSurvivedText.text = "Time Survived: " + Math.Round(TitleManager.saveData.timeSurvived).ToString() + " secs";
        // totalCrystals.text = "Crystals: " + TitleManager.saveData.crystalCount.ToString();
        //totalHealPotions.text = "HealPotions: " + TitleManager.saveData.healpotionCount.ToString();
    }
}
