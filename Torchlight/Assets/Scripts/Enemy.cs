using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // References to components
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] GameObject energyCrystalPrefab;
    [SerializeField] GameObject goldCoinPrefab;

    // Enemy properties
    [SerializeField] private float speed = 1f;
    [SerializeField] public bool isTrackingPlayer = true;
    [SerializeField] private float enemyHP = 10f;
    [SerializeField] private float enemyKillCount = 0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponents();
        PlayIdleAnimation();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveTowardsPlayer();
        FlipSprite();
        CheckForPlayer();
    }

    // Get references to components
    private void GetComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Play the Idle animation
    private void PlayIdleAnimation()
    {
        animator.Play("Warrior_Idle");
    }

    // Move the enemy towards the player
    private void MoveTowardsPlayer()
    {
        Vector3 destination = player.transform.position;
        Vector3 source = transform.position;
        Vector3 direction = destination - source;

        if (!isTrackingPlayer)
        {
            direction = new Vector3(1, 0, 0);
        }

        direction.Normalize();
        transform.position += direction * Time.deltaTime * speed;
    }

    // Flip the sprite based on the direction
    private void FlipSprite()
    {
        if (isTrackingPlayer)
        {
            if (transform.position.x < player.transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (transform.position.x > player.transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    // Check if the enemy has found the player and change the animator's state accordingly
    private void CheckForPlayer()
    {
        if (isTrackingPlayer)
        {
            animator.Play("Warrior_Run");
        }
        else
        {
            animator.Play("Warrior_Walk");
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Increase the kill count
        enemyKillCount++;

        // Drop a crystal or a gold coin at the death position
        if (Random.value < 0.5f)
        {
            // Drop a crystal
            Instantiate(energyCrystalPrefab, transform.position, Quaternion.identity.normalized);
            
        }
        else
        {
            // Drop a gold coin
            Instantiate(goldCoinPrefab, transform.position, Quaternion.identity.normalized);
           
        }

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
