using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    [SerializeField] private WordBank wordBank;
    [SerializeField] private AudioManager sound;
    private TextMeshPro wordOutput = null;


    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    public Enemy currentTarget;
    private List<Enemy> activeEnemies = new List<Enemy>();



    private void Update()
    {
        CheckInput();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord(1);
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;

    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (!activeEnemies.Contains(enemy))
        {
            activeEnemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }

        if (enemy.enemyType == Enemy.EnemyType.Boss)
        {
            FindObjectOfType<BossSpawner>()?.SetNextBossWordToBoss();
        }
    }

    public void EnterLetter(string typedLetter)
    {
        if (currentTarget == null)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                if (enemy != null && enemy.IsCorrectLetter(typedLetter))
                {
                    currentTarget = enemy;
                    break;
                }
            }
        }

        if (currentTarget != null)
        {
            if (currentTarget.IsCorrectLetter(typedLetter))
            {
                currentTarget.RemoveLetter();
                sound.Typing();

                if (currentTarget.IsWordComplete())
                {
                    // Boss tidak dilempar dagger, langsung lanjut kata berikutnya
                    if (currentTarget.enemyType != Enemy.EnemyType.Boss)
                    {
                        FindObjectOfType<PlayerController>().ThrowDagger(currentTarget);
                    }
                    else
                    {
                        FindObjectOfType<BossSpawner>()?.SetNextBossWordToBoss();
                    }

                    currentTarget = null;
                }
            }
        }
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;

            if (keyPressed.Length == 1)
            {
                EnterLetter(keyPressed);
            }
        }
    }
    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    // private bool IsWrongLetter(string letter)
    // {
    //     return 
    // }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    public bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
