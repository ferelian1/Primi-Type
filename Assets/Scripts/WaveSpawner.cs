using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints; // 0 dan 1 = enemy biasa, 2 = boss
    [SerializeField] private GameObject[] enemyPrefabs; // 0 = wave1, 1 = wave2, dst.
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private WordBank wordBank;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private NavMeshSurface navmesh;

    [SerializeField] private float SpawnRatePerSecond = 5.5f;

    private int currentWave = 1;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
        navmesh.BuildNavMesh();
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(2f);
        while (currentWave <= 5)
        {
            waveText.text = $"Wave {currentWave}";
            yield return new WaitForSeconds(2f);
            waveText.text = "";

            isSpawning = true;

            if (currentWave < 5)
            {
                int enemyCount = 10 + (currentWave - 1) * 5;
                for (int i = 0; i < enemyCount; i++)
                {
                    SpawnEnemy(currentWave);
                    yield return new WaitForSeconds(SpawnRatePerSecond);
                }
            }
            else
            {
                // WAVE 5 - Boss
                waveText.text = "⚠️ A BOSS IS COMING ⚠️";
                yield return new WaitForSeconds(3f);
                waveText.text = "";

                SpawnBoss();
            }

            isSpawning = false;

            // Tunggu semua enemy mati
            while (GameObject.FindObjectsOfType<Enemy>().Length > 0)
            {
                yield return null;
            }

            currentWave++;
            yield return new WaitForSeconds(2f);
        }

        waveText.text = "YOU WIN!";
    }

    void SpawnEnemy(int waveLevel)
    {
        int spawnIndex = Random.Range(0, 2); // hanya 2 titik biasa
        GameObject prefab = enemyPrefabs[waveLevel - 1];
        Transform spawnPoint = spawnPoints[spawnIndex];

        GameObject enemyObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        Enemy enemy = enemyObj.GetComponent<Enemy>();


        string word = wordBank.GetWord(waveLevel);
        enemy.SetWord(word);
    }

    void SpawnBoss()
    {
        Transform bossPoint = spawnPoints[2]; // spawn point khusus boss
        GameObject bossObj = Instantiate(bossPrefab, bossPoint.position, Quaternion.identity, bossPoint);
        Enemy bossEnemy = bossObj.GetComponent<Enemy>();
        

        // boss punya 10 kata random
        string word = "";
        for (int i = 0; i < 10; i++)
        {
            word += wordBank.GetRandomBossWord() + " ";
        }

        bossEnemy.SetWord(word.Trim());
    }
}
