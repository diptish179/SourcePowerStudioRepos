using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    //[SerializeField] PlayerController player;
    [SerializeField] int minPowerIncrease = 1;
    [SerializeField] int maxPowerIncrease = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            int powerIncrease = Random.Range(minPowerIncrease, maxPowerIncrease + 1);
            if (powerIncrease < 0 && player.currentPower <= 0)
            {
                
            }
            else
            {
                player.currentPower += powerIncrease;
                if (player.currentPower > player.maxPower)
                {
                    player.currentPower = player.maxPower;
                }
                else if (player.currentPower < 0)
                {
                    player.currentPower = 0;
                }
            }
            Destroy(gameObject);
        }
    }
}
