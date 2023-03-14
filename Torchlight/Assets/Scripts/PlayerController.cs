using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float playerSize = 3f;
    private Vector2 movement;
    private Rigidbody2D rb;
    Animator animator;
    SpriteRenderer playerRendered;
    [SerializeField] GameObject LightBall;
    public GameObject MageAtk1Prefab;


    private bool isAttacking = false; // New variable to track attack state

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float inputRun = Input.GetAxis("Jump");
        bool inputAtk1 = Input.GetButtonDown("Fire1");
        bool inputAtk2 = Input.GetButtonDown("Fire2");
        bool inputAtk3 = Input.GetButtonDown("Fire3");
        bool inputAtk4 = Input.GetButtonDown("Sprint");
       // bool inputTeleport = Input.GetButtonDown("Submit");

        

        if (!isAttacking) // Only allow movement if not attacking
        {
            transform.position += new Vector3(inputX, inputY, 0) * moveSpeed * Time.deltaTime;
        }
        //transform.position += new Vector3(inputX, inputY, 0) * moveSpeed * Time.deltaTime;

        if (inputX != 0) // To make the sprite face in the moving direction
        {
            transform.localScale = new Vector3(inputX > 0 ? -playerSize : playerSize, playerSize, playerSize);
        }
        if (inputAtk1 && !isAttacking)
        {
            animator.Play("Mage_Attack1");
            isAttacking = true;
            LightBall.SetActive(false);

            // Spawn circular sprite at player's position
            GameObject circularSprite = Instantiate(MageAtk1Prefab, transform.position, Quaternion.identity);

            // Generate random direction for circular sprite to travel in
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            // Get the circular sprite's Rigidbody2D component
            Rigidbody2D circularSpriteRigidbody = circularSprite.GetComponent<Rigidbody2D>();

            // Set the circular sprite's velocity to travel in the random direction at constant speed
            circularSpriteRigidbody.velocity = randomDirection * moveSpeed;
        }
        else if (inputAtk2 && !isAttacking)
        {
            animator.Play("Mage_Attack2");
            isAttacking = true;
            LightBall.SetActive(false);
        }
        else if (inputAtk3 && !isAttacking)
        {
            animator.Play("Mage_LightCharge");
            isAttacking = true;
            LightBall.SetActive(false);
        }
        else if (inputAtk4 && !isAttacking)
        {
            animator.Play("Mage_LightBall");
            isAttacking = true;
            LightBall.SetActive(true); // Activate the LightBall game object
        }

        else if (isAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("Mage_LightBall")) // Check if the current animation is "Mage_LightBall"
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) // Check if the animation is finished
            {
                isAttacking = false; // Reset attack state
                LightBall.SetActive(false); // Deactivate the LightBall game object
            }
            else
            {
                LightBall.SetActive(true); // Activate the LightBall game object
            }
        }

        
        else if (isAttacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) 
        {
            // Check if an attack is in progress and continue playing the attack animation until it is complete (Wait for anim to complete cycle)
        }
        else if (inputX == 0 && inputY == 0)
        {
            animator.Play("Mage_Idle");
            isAttacking = false; // Reset attack state
            LightBall.SetActive(false); // Deactivate the LightBall game object
        }
        else if (inputRun == 0)
        {
            animator.Play("Mage_Walk");
            moveSpeed = 3;
            isAttacking = false; // Reset attack state
            LightBall.SetActive(false); // Deactivate the LightBall game object
        }
        else if (inputRun != 0 && (inputX != 0 || inputY != 0))
        {
            animator.Play("Mage_Run");
            moveSpeed = 5;
            isAttacking = false; // Reset attack state
            LightBall.SetActive(false); // Deactivate the LightBall game object
        }


    } 


}





