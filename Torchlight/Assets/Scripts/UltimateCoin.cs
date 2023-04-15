using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateCoin : MonoBehaviour
{
    public int coinValue = 1; // value of the coin

    // OnTriggerEnter2D is called when a collider enters the trigger zone of the game object
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the PlayerController component from the object that collided with the coin
        PlayerController player = collision.GetComponent<PlayerController>();

        // If the object has the PlayerController component
        if (player != null && player.currentUltimateCoin < player.maxUltimateCoin)
        {
            // Increase the current ultimate coin count of the player
            player.currentUltimateCoin += coinValue;

            // If the current ultimate coin count is equal to the max ultimate coin count
            if (player.currentUltimateCoin == player.maxUltimateCoin)
            {
                // Set the ultimateReady flag of the player to true
                player.ultimateReady = true;
            }

            // Destroy the coin game object
            Destroy(gameObject);
        }
    }
}