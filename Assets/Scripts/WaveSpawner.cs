using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class WaveSpawner : MonoBehaviour {
    [SerializeField] public Transform[] spawnPoints; // 0 dan 1 = enemy biasa, 2 = boss
    [SerializeField] private GameObject[] enemyType; // List untuk wave 1
    [SerializeField] private WordBank wordBank;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private NavMeshSurface navmesh;

    [Header("Initialize Data")] //AWHFBAWHFBAWHFBAWF TAYO
    [Tooltip("PILIH LEVEL!")]
    [SerializeField] private int level;
    [Tooltip("Frekuensi wave awal keluar monsternya")]
    [SerializeField] private float SpawnRatePerSecond = 2.5f;
    [Tooltip("MAU ADA BERAPA WAVENYA")]
    [SerializeField] private int totalWave;

    private bool isPaused = false;
    private int currentWave = 1;
    private bool isSpawning = false;

    void Start() {
        StartCoroutine(StartNextWave());
        navmesh.BuildNavMesh();
    }

    public void PauseSpawner() {
        isPaused = true;
    }

    public void ResumeSpawner() {
        isPaused = false;
    }


    IEnumerator StartNextWave() {
        yield return new WaitForSeconds(2f);


        while (currentWave <= totalWave) {
            waveText.text = $"Wave {currentWave}";
            yield return new WaitForSeconds(2f);
            waveText.text = "";

            isSpawning = true;

            if (currentWave < totalWave + 1) {
                int enemyCount = 10 + (currentWave - 1) * 5;
                for (int i = 0; i < enemyCount; i++) {

                    while (isPaused) {
                        yield return null;
                    }

                    SpawnEnemy(currentWave);
                    yield return new WaitForSeconds(SpawnRatePerSecond);
                }
            }


            isSpawning = false;

            // Tunggu semua enemy mati
            while (GameObject.FindObjectsOfType<Enemy>().Length > 0) {
                yield return null;
            }

            currentWave++;
            SpawnRatePerSecond -= .5f;
            yield return new WaitForSeconds(2f);
        }

        FindObjectOfType<GameManager>().WinResult();
        FindObjectOfType<AudioManager>().Winning();
    }

    void SpawnEnemy(int waveLevel) {
        GameObject enemySpawn = enemyType[Random.Range(0, enemyType.Length)];
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject prefab = enemySpawn;
        Transform spawnPoint = spawnPoints[spawnIndex];

        GameObject enemyObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        
    }




}
