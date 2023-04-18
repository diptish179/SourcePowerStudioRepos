using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float Atk1Speed = 7f;
    public float playerSize = 3f;
    public double currentHP;
    public double maxHP = 6;
    public bool isInvincible;
    public double currentPower; // New variable to track power
    public double maxPower = 100;
    public bool isOutOfPower;
    public float currentUltimateCoin;
    public float maxUltimateCoin = 10;
    public bool ultimateReady;
    public int goldCoins;
    public float powerCost;
    private float healAmount;


    private Vector2 movement;
    private Rigidbody2D rb;
    Animator animator;
    SpriteRenderer playerRendered;
    [SerializeField] GameObject LightBall;
    [SerializeField] GameObject MageLightChargePrefab;
    public GameObject MageAtk1Prefab;
    public GameObject MageAtk2Prefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Image flashImage;

    Material material;

    private bool isAttacking = false; // New variable to track attack state

    private void Start()
    {
        currentHP = maxHP;
        currentPower = maxPower;
        currentUltimateCoin = 0;
        maxUltimateCoin = 10;
        goldCoins = 0;
        TitleManager.saveData.ultimateUsedCount = 0;
        TitleManager.saveData.goldCoins = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        material = spriteRenderer.material;
    }

    [System.Obsolete]
    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float inputRun = Input.GetAxis("Jump");
        bool inputAtk1 = Input.GetButtonDown("Fire1");
        bool inputAtk2 = Input.GetButtonDown("Fire2");
        bool inputAtk3 = Input.GetButtonDown("Fire3");
        bool inputAtk4 = Input.GetButtonDown("Sprint");


        // Only allow movement if not attacking
        if (!isAttacking)
        {
            transform.position += new Vector3(inputX, inputY, 0) * moveSpeed * Time.deltaTime;
        }

        // To make the sprite face in the moving direction
        if (inputX != 0)
        {
            transform.localScale = new Vector3(inputX > 0 ? -playerSize : playerSize, playerSize, playerSize);
        }

        if (inputAtk1 && !isAttacking && !isOutOfPower)
        {
            StartCoroutine(AttackWithCircularSprite());
            powerCost = 4;
        }

        if (inputAtk2 && !isAttacking && !isOutOfPower)
        {
            StartCoroutine(AttackWithCircularSprite2());
            powerCost = 7;
        }

        if (inputAtk3 && !isAttacking && !isOutOfPower)
        {
            StartCoroutine(AttackWithRectangularSprite());
            powerCost = 3;
        }

        if (inputAtk4 && !isAttacking && !isOutOfPower && ultimateReady)
        {
            TitleManager.saveData.ultimateUsedCount++;
            powerCost = 10;
            animator.Play("Mage_LightBall");
            isAttacking = true;
            DisableEnemyTracking();  // set istracking property of all enemies active in the scene to false
            currentUltimateCoin -= maxUltimateCoin;
            ultimateReady = false;
            LightBall.SetActive(true); // Activate the LightBall game object                                      
            StartCoroutine(FlashScreen());
        }



        if (isAttacking)
        {
            currentPower -= powerCost * Time.unscaledDeltaTime;
            if (currentPower <= 0)
            {
                isOutOfPower = true;
            }
            else
            {
                isOutOfPower = false;
            }

        }
        else
        {
            if (inputX == 0 && inputY == 0)
            {
                animator.Play("Mage_Idle");
            }
            else if (inputRun == 0)
            {
                animator.Play("Mage_Walk");
                moveSpeed = 3;
            }
            else if (inputRun != 0 && (inputX != 0 || inputY != 0))
            {
                animator.Play("Mage_Run");
                moveSpeed = 5;
            }

            LightBall.SetActive(false);
        }



    }

    internal void PlayerHeal()
    {
        StartCoroutine(HealCouroutine());
        if (currentHP == maxHP)
        {
            // The player is already at full health, no further action needed
        }
        else if (currentHP <= 0)
        {
            currentHP = 0.75; // This seems like an unusual behavior, are you sure you want to set it to 0.5 instead of 0?
        }
        else
        {
            // Calculate the amount to heal the player by
            healAmount = (float)(0.25f * maxHP);

            // Ensure the heal amount won't exceed the amount needed to reach full health
            if (currentHP + healAmount > maxHP)
            {
                healAmount = (float)(maxHP - currentHP);
            }
            // Apply the heal
            currentHP += healAmount;
        }
    }


        IEnumerator HealCouroutine()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
    
    }

    public bool OnDamage()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(InvincibilityCoroutine());
            if (currentHP-- <= 0)
            {
                //TitleManager.saveData.deathCount++;
                Destroy(gameObject);

                // Check if the player gameobject is still active in the scene
                if (GameObject.Find("Player") == null)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
            return true;
        }
        return false;
    }

    private IEnumerator FlashScreen()
    {
        // Wait for one second
        yield return new WaitForSeconds(1f);
        // Set the color to white
        flashImage.color = Color.white;        

        // Gradually fade the color back to transparent over one second
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            flashImage.color = Color.Lerp(Color.white, Color.clear, t);
            yield return null;
        }

        // Deactivate the LightBall game object
        LightBall.SetActive(false);

        // Set the isAttacking flag to false to allow another attack
        isAttacking = false;
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        spriteRenderer.color = Color.red;         
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;     
        isInvincible = false;
    }

    public void DisableEnemyTracking()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                enemy.isTrackingPlayer = false;
            }
        }
    }

    IEnumerator AttackWithCircularSprite()
    {
        animator.Play("Mage_Attack1");
        isAttacking = true;
        LightBall.SetActive(false);

        // Wait until the animation is completed
         yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Calculate direction from player to mouse cursor
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Calculate the angle between the direction vector and the x-axis, and set the sprite's rotation to that angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //circularSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Spawn circular sprite at player's position
        GameObject circularSprite = Instantiate(MageAtk1Prefab, transform.position, Quaternion.Euler(0, 0, angle));

        // Get the circular sprite's Rigidbody2D component
        Rigidbody2D circularSpriteRigidbody = circularSprite.GetComponent<Rigidbody2D>();

        // Set the circular sprite's velocity to travel in the direction at constant speed
        circularSpriteRigidbody.velocity = direction.normalized * Atk1Speed;

        isAttacking = false;
    }

    IEnumerator AttackWithCircularSprite2()
    {
        animator.Play("Mage_Attack2");
        isAttacking = true;
        LightBall.SetActive(false);

        // Wait until the animation is completed
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Calculate direction from player to mouse cursor
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Spawn circular sprite at player's position
        GameObject circularSprite = Instantiate(MageAtk2Prefab, transform.position, Quaternion.Euler(0,0,angle));       

        // Get the circular sprite's Rigidbody2D component
        Rigidbody2D circularSpriteRigidbody = circularSprite.GetComponent<Rigidbody2D>();

        // Set the circular sprite's velocity to travel in the direction at constant speed
        circularSpriteRigidbody.velocity = direction.normalized * Atk1Speed;

        isAttacking = false;
    }

    IEnumerator AttackWithRectangularSprite()
    {
        animator.Play("Mage_LightCharge");
        isAttacking = true;
        LightBall.SetActive(false);

        // Wait until the animation is completed
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Determine the offset position based on the player sprite's facing direction
        float offset = transform.localScale.x > 0 ? -1.5f : 1.5f;
        Vector3 spawnPosition = transform.position + new Vector3(offset, 0.5f, 0);

        // Instantiate the rectangular sprite at the offset position
        GameObject rectangularSprite = Instantiate(MageLightChargePrefab, spawnPosition, Quaternion.identity);

        // Determine the horizontal direction based on the player sprite's facing direction
        Vector2 direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;

        // Set the velocity of the rectangular sprite to move in the determined direction
        Rigidbody2D rectangularSpriteRigidbody = rectangularSprite.GetComponent<Rigidbody2D>();
        rectangularSpriteRigidbody.velocity = direction * Atk1Speed;

        // Set isAttacking to false after the attack is completed
        isAttacking = false;
    }





}





