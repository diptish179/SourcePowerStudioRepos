using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : MonoBehaviour
{
    AudioSource goldCoinSFX;
    // Start is called before the first frame update
    void Start()
    {
        goldCoinSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player != null)
        {

            TitleManager.saveData.goldCoins++;
            goldCoinSFX.Play();
            DestroyObject(gameObject, 0.5f);
           
        }

        


    }
}
