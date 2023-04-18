using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject warrior1;
    [SerializeField] GameObject warrior2;
    [SerializeField] GameObject warrior3;
    [SerializeField] GameObject player;
    [SerializeField] GameObject healItem0;

    [SerializeField] int waveoffset = 20;
    [SerializeField] int hordeoffset = 20;
    [SerializeField] int healItemoffset = 15;
    [SerializeField] TMP_Text timerTxt;

    float totalTime = 0f;
    

    public void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        timerTxt.text = "[" + minutes.ToString("00") + ":" + seconds.ToString("00") +"]";
    }


    void Start()
    {
        // Check which level is currently loaded
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            // Start spawning enemies using the SpawnEnemy1Coroutine if Level 1 is loaded
            StartCoroutine(SpawnEnemyWave1Coroutine());
            StartCoroutine(SpawnHealItemCoroutine());
            TitleManager.saveData.timeSurvived = 0;
        }
       
    }

    public void Update()
    {
        totalTime += Time.deltaTime;
        UpdateLevelTimer(totalTime);
        TitleManager.saveData.timeSurvived = totalTime;
    }

    private IEnumerator SpawnHealItemCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            SpawnHealItem();            
        }


    }

    void SpawnHealItem()
    {
        Vector3 healItemSpawnPosition = Random.insideUnitCircle.normalized * healItemoffset;

        healItemSpawnPosition += player.transform.position;
        Instantiate(healItem0, healItemSpawnPosition, Quaternion.identity);

    }


    private IEnumerator SpawnEnemyWave1Coroutine()
    {
        int numEnemies = 2; // Start with 2 enemies
        int killCount = TitleManager.saveData.killCount;

        while (true)
        {
            if (killCount >= 100) // Increase the number of enemies after every 100 kills
            {
                numEnemies++;
                killCount -= 100;
            }

            SpawnEnemies(warrior1, numEnemies);
            yield return new WaitForSeconds(2f);
            SpawnEnemies(warrior2, numEnemies);
            yield return new WaitForSeconds(3f);
            SpawnEnemies(warrior3, numEnemies);
            yield return new WaitForSeconds(3f);
            SpawnEnemies(warrior1, 1, false);
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

            for (int i = 0; i < numEnemies; i++)
            {
                SpawnEnemies(warrior2, 1);
                SpawnEnemies(warrior3, 1);
                yield return new WaitForSeconds(4f);
            }

            SpawnEnemies(warrior1, 1);
            SpawnEnemies(warrior2, 2);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(warrior1, 1);
            SpawnEnemies(warrior2, 1);
            SpawnEnemies(warrior3, 2);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(warrior3, 2);
            SpawnEnemies(warrior2, 2);
            SpawnEnemies(warrior1, 1);
        }
    }



    // The enemies will follow the player when isTracking is true, or they will move to the right when isTracking is false.
    void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies, bool isTracking = true)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition;

            // If isTracking is true, set the spawn position to be anywhere around the player
            if (isTracking)
            {
                Vector2 randomPointOnCircle = Random.insideUnitCircle.normalized;
                spawnPosition = player.transform.position + new Vector3(randomPointOnCircle.x, randomPointOnCircle.y, 0) * waveoffset;
            }
            // If isTracking is false, set the spawn position to be to the left of the player in the same horizontal axis
            else
            {
                spawnPosition = player.transform.position -  new Vector3(hordeoffset,hordeoffset,0);
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

