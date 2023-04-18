using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{

    AudioSource playerHealSFX;
    //public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        playerHealSFX = GetComponent<AudioSource>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }


    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player player = collision.GetComponent<Player>();
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && player.currentHP < player.maxHP)
        {
            /// Add 2 hp to player 
            //Debug.Log("Picked up 1 HP");
            TitleManager.saveData.healpotionCount++;
            player.PlayerHeal();
            playerHealSFX.Play();
            DestroyObject(gameObject, 0.5f);
        }
    }

}
