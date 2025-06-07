using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEditor.Rendering;

public class Enemy : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField] private TextMeshPro wordText;
    [SerializeField] private float speed;
    private string currentWord = "muffins";
    private string remainingWord;

    private Transform targetPlayer;
    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;


        // Cari player berdasarkan tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        if (playerObject != null)
        {
            targetPlayer = playerObject.transform;
        }
        
        FindObjectOfType<Typer>().RegisterEnemy(this);
    }

    private void Update()
    {
        if (targetPlayer != null && agent != null)
        {
            agent.SetDestination(targetPlayer.position);
        }

    
    }

    public void SetWord(string word)
    {
        currentWord = word;
        remainingWord = word;
        UpdateWordText();
    }

    public bool IsCorrectLetter(string input)
    {
        return remainingWord.StartsWith(input);
    }

    public void RemoveLetter()
    {
        if (remainingWord.Length > 0)
        {
            remainingWord = remainingWord.Substring(1);
            UpdateWordText();

            if (remainingWord.Length == 0)
            {
                Die();
            }
        }
    }

    private void UpdateWordText()
    {
        if (wordText != null)
        {
            wordText.text = remainingWord;
        }
    }

    private void Die()
    {
        FindObjectOfType<Typer>().UnregisterEnemy(this);
        //nanti tambah animasi mati tapi bentar
        Destroy(gameObject);
    }
}
