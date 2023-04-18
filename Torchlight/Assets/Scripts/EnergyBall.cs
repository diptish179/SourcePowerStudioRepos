using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] int minPowerIncrease = 1;
    [SerializeField] int maxPowerIncrease = 10;
    AudioSource energyBallSFX;
    void Start()
    {
        energyBallSFX = GetComponent<AudioSource>();
    }

    [System.Obsolete]
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            int powerIncrease = Random.Range(minPowerIncrease, maxPowerIncrease + 1);
            player.currentPower += powerIncrease;
            if (player.currentPower > player.maxPower)
            {
                player.currentPower = player.maxPower;
            }
                      

            if (player.isOutOfPower)
            {
                player.isOutOfPower = false;
                player.currentPower += powerIncrease;
            }

            energyBallSFX.Play();
            DestroyObject(gameObject, 0.5f);
        }
    }
}
