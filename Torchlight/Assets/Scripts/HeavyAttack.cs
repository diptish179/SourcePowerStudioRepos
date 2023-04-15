using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : MonoBehaviour
{
    public float damage = 5f; // Damage to apply to the enemy
    public float lifespan = 10f; // Time before the projectile is destroyed
    private bool isDestroyed = false; // Flag to track if the projectile is destroyed
    public GameObject splitProjectilePrefab; // Reference to the split projectile prefab to be spawned

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

                // Spawn 8 split projectiles
                for (int i = 0; i < 8; i++)
                {
                    float angle = i * Mathf.PI / 4f;
                    Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    GameObject splitProjectile = Instantiate(splitProjectilePrefab, transform.position, Quaternion.Euler(0, 0, i*45));
                    //Debug.Log("angles:" + i  + ":" + angle);
                    splitProjectile.GetComponent<Rigidbody2D>().velocity = direction * GetComponent<Rigidbody2D>().velocity.magnitude;
                }

                Destroy(gameObject); // Destroy the original projectile game object
            }
        }
    }
}

