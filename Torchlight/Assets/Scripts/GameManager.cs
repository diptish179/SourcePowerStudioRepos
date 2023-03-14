using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject warrior1;
    [SerializeField] GameObject warrior2;
    [SerializeField] GameObject warrior3;
    [SerializeField] GameObject player;

    [SerializeField] int waveoffset = 15;
    [SerializeField] int hordeoffset = 15;




    void Start()
    {
        // Check which level is currently loaded
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            // Start spawning enemies using the SpawnEnemy1Coroutine if Level 1 is loaded
            StartCoroutine(SpawnEnemyWave1Coroutine());
        }
       
    }


    public void Update()
    {
       

    }
        



    private IEnumerator SpawnEnemyWave1Coroutine()
    {
        SpawnEnemies(warrior1, 2);
        yield return new WaitForSeconds(2f);
        SpawnEnemies(warrior2, 2);
        yield return new WaitForSeconds(3f);
        SpawnEnemies(warrior1, 1,false);
        yield return new WaitForSeconds(3f);
        SpawnEnemies(warrior1, 5);
        yield return new WaitForSeconds(4f);
        SpawnEnemies(warrior1, 3);
        SpawnEnemies(warrior3, 3);
        yield return new WaitForSeconds(4f);
        SpawnEnemies(warrior1, 1, false);
        SpawnEnemies(warrior2, 1, false);
        SpawnEnemies(warrior3, 1, false);
        yield return new WaitForSeconds(4f);
        SpawnEnemies(warrior2, 5);
        SpawnEnemies(warrior3, 4);

    }

    
    //The enemies will follow the player when isTracking is true, or they will move to the right when isTracking is false.

    void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies, bool isTracking = true)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition;

            // If isTracking is true, set the spawn position to be near the player
            if (isTracking)
            {
                spawnPosition = Random.insideUnitCircle.normalized * waveoffset;
                spawnPosition += player.transform.position;
            }
            // If isTracking is false, set the spawn position to be to the right of the player
            else
            {
                spawnPosition = player.transform.position + Vector3.right * (-hordeoffset);
            }

            // Instantiate the enemy at the calculated position
            GameObject enemyobject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // If isTracking is false, set the enemy's isTrackingPlayer property to false
            if (!isTracking)
            {
                Enemy enemy = enemyobject.GetComponent<Enemy>();
                enemy.isTrackingPlayer = false;
            }
        }
    }






}

