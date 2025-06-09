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
    [SerializeField] private TextMeshPro wordText;
    [SerializeField] private float speed;


    [Header("Enemy Type")]
    private Animator anim;
    public EnemyType enemyType;

    public enum EnemyType { alive, notAlive, Boss }
    private string currentWord = string.Empty;
    private string remainingWord = string.Empty;

    private const string IS_DEATHTRIGGER = "isDeathTrigger";
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


        if (enemyType == EnemyType.alive || enemyType == EnemyType.Boss)
        {
            anim = GetComponentInChildren<Animator>();
        }
        else
        {

        }
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
                //agent.SetDestination(targetPlayer.position);
                Vector3 targetPos = targetPlayer.position;


                targetPos.x = transform.position.x;
                targetPos.y = transform.position.y;


                agent.SetDestination(targetPos);
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
        if (other.CompareTag(PLAYER_TAG))
        {
            // Panggil fungsi ReduceHealth pada PlayerController
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ReduceHealth();
                Die();
            }
        }
    }

    public bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
    public void Die()
    {
        if (enemyType != EnemyType.Boss)
        {
            FindObjectOfType<Typer>().UnregisterEnemy(this);
            //nanti tambah animasi mati tapi bentar
            Destroy(gameObject);
        }
    }

    public void Death()
    {
        if (enemyType != EnemyType.Boss && enemyType == EnemyType.alive)
        {
            FindObjectOfType<Typer>().UnregisterEnemy(this);
            StartCoroutine(WaitBeforeDeath());

        }
    }

    private IEnumerator WaitBeforeDeath()
    {
        anim.SetBool(IS_DEATHTRIGGER, true);
        agent.isStopped = true;
        yield return new WaitForSeconds(.8f);
        Destroy(gameObject);

    }
}
