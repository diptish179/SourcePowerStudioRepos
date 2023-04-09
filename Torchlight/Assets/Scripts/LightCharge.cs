using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCharge : MonoBehaviour
{

    public float damage = 5f; // Damage to apply to the enemy
    public float lifespan = 10f; // Time before the projectile is destroyed
    private bool isDestroyed = false; // Flag to track if the projectile is destroyed

    void Start()
    {
        Destroy(gameObject, lifespan); // Destroy the projectile after a certain time
    }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null && !isDestroyed) // Check if the enemy has an EnemyController component and if the projectile is not already destroyed
            {
                enemy.TakeDamage(damage); // Apply damage to the enemy

                isDestroyed = true; // Set the flag to indicate that the projectile is destroyed
                Destroy(gameObject); // Destroy the projectile game object
            }
        }
    }
   
}
