using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEditor.Rendering;
using System;


public class Enemy : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string PLAYERTRIGGER_TAG = "PlayerTrigger";
    [SerializeField] private TextMeshPro wordText;
    [SerializeField] private float speed;

    // [Header("Enemy Type")]
    public EnemyType enemyType;

    public enum EnemyType { alive, notAlive, Boss }
    private string currentWord = string.Empty;
    private string remainingWord = string.Empty;


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

        if (enemyType == EnemyType.notAlive)
        {

            Vector3 targetPos = targetPlayer.position;


            targetPos.x = transform.position.x;
            targetPos.y = transform.position.y;


            agent.SetDestination(targetPos);
        }
        else if (enemyType == EnemyType.alive || enemyType == EnemyType.Boss)
        {
            if (enemyType == EnemyType.Boss)
            {
                agent.speed -= 5;
            }
            if (targetPlayer != null && agent != null)
            {
                agent.SetDestination(targetPlayer.position);
            }
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

            if (remainingWord.Length == 0 && enemyType != EnemyType.Boss)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYERTRIGGER_TAG)) 
        {
            // Panggil fungsi ReduceHealth pada PlayerController
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                player.ReduceHealth();
                Die();
            }
        }
    }
    private void Die()
    {
        FindObjectOfType<Typer>().UnregisterEnemy(this);
        //nanti tambah animasi mati tapi bentar
        Destroy(gameObject);
    }
}
