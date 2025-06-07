using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints; // 0 dan 1 = enemy biasa, 2 = boss
    [SerializeField] private GameObject[] wave1Enemies; // List untuk wave 1
    [SerializeField] private GameObject[] wave2Enemies; // List untuk wave 2
    [SerializeField] private GameObject[] wave3Enemies; // List untuk wave 3
    [SerializeField] private GameObject[] wave4Enemies; // List untuk wave 4    
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
        GameObject prefab = GetEnemyPrefabForWave(waveLevel);
        Transform spawnPoint = spawnPoints[spawnIndex];

        GameObject enemyObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        Enemy enemy = enemyObj.GetComponent<Enemy>();


        string word = wordBank.GetWord(waveLevel);
        enemy.SetWord(word);
    }


    GameObject GetEnemyPrefabForWave(int waveLevel)
    {
        GameObject[] selectedWaveEnemies = new GameObject[0];

        switch (waveLevel)
        {
            case 1:
                selectedWaveEnemies = wave1Enemies;
                break;
            case 2:
                selectedWaveEnemies = wave2Enemies;
                break;
            case 3:
                selectedWaveEnemies = wave3Enemies;
                break;
            case 4:
                selectedWaveEnemies = wave4Enemies;
                break;
        }

        // Pilih secara acak dari daftar enemy yang ada untuk wave tersebut
        return selectedWaveEnemies[Random.Range(0, selectedWaveEnemies.Length)];
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
