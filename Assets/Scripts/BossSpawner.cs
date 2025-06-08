using System.Collections;
using UnityEngine;
using TMPro;
using Unity.AI.Navigation;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private WordBank wordBank;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private NavMeshSurface navMesh;

    private Enemy bossEnemy;

    void Start()
    {
        StartCoroutine(SpawnBossSequence());
        navMesh.BuildNavMesh();
    }

    private IEnumerator SpawnBossSequence()
    {
        waveText.text = " FINAL BATTLE BEGINS!!!";
        yield return new WaitForSeconds(3f);
        waveText.text = "";

        GameObject bossObj = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossEnemy = bossObj.GetComponent<Enemy>();

        wordBank.GenerateBossWords(10);
        SetNextBossWordToBoss();

        yield return new WaitUntil(() => bossEnemy == null); // Tunggu kata selesai

        yield return new WaitForSeconds(1.5f);
        waveText.text = "...Not yet!!...";
        yield return new WaitForSeconds(2.5f);
        waveText.text = "";

        if (bossObj != null)
        {
            // Boss belum mati, tambah 10 kata lagi
            wordBank.GenerateBossWords(10);
            SetNextBossWordToBoss();
        }

        // Tunggu boss mati total
        yield return new WaitUntil(() => bossEnemy == null);

        waveText.text = "YOU WIN!";
    }

    private string GenerateWords(int count)
    {
        string result = "";
        for (int i = 0; i < count; i++)
        {
            result += wordBank.GetRandomBossWord() + " ";
        }
        return result.Trim();
    }

    public void SetNextBossWordToBoss()
    {
        if (bossEnemy != null)
        {
            string nextWord = wordBank.GetNextBossWord();
            if (!string.IsNullOrEmpty(nextWord))
            {
                bossEnemy.SetWord(nextWord);
            }
            else
            {
                bossEnemy = null;
            }
        }
    }

}
