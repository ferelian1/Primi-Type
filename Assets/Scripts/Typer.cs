using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    [SerializeField] private WordBank wordBank;
    [SerializeField] private TextMeshPro wordOutput = null;
    
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    private Enemy currentTarget;
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

                if (currentTarget.gameObject == null)
                {
                    // Enemy destroyed, unregister
                    UnregisterEnemy(currentTarget);
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

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
