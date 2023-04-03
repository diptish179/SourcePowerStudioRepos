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

    [SerializeField] int waveoffset = 15;
    [SerializeField] int hordeoffset = 15;
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

        timerTxt.text = "Time survived [" + minutes.ToString("00") + ":" + seconds.ToString("00") +"]";
    }


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
        totalTime += Time.deltaTime;
        UpdateLevelTimer(totalTime);

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
        while (true)
        {
            SpawnEnemies(warrior2, 1);
            SpawnEnemies(warrior3, 1);
            yield return new WaitForSeconds(4f);
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

