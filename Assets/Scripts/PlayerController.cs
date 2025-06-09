using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Typer typer;
    [SerializeField] private Animator anim;
    [SerializeField] private int health = 3;
    [SerializeField] private Image[] heartImages; // Array of heart images
    [SerializeField] private Sprite fullHeart;   // Full heart sprite
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Image playerHurtUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject daggerPrefab;  
    [SerializeField] private Transform throwPoint; //

    private const string ANIM_THROW = "Throw";
    private const string ANIM_IDLE = "Idle";
    private const string ANIM_LOSE = "Lose";

    private AudioManager playerSound;
    private GameObject currentDagger;
    private void Start()
    {
        playerSound = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {

        if (health == 0)
        {
            Death();

        }
        else
        {
            UpdateHealthUI();  // Update UI based on health
        }

        if (playerHurtUI.color.a != 0)
        {
            float hurtUIAlpha = playerHurtUI.color.a;
            hurtUIAlpha -= 0.05f;
        }

    }

    private void UpdateHealthUI()
    {
        // Loop through all heart images
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < health)
            {
                heartImages[i].sprite = fullHeart;  // Set full heart if player has that health
            }
            else
            {
                heartImages[i].sprite = emptyHeart;  // Set empty heart if health is lower
            }
        }
    }

    private void Death()
    {
        anim.Play(ANIM_LOSE);
        gameManager.LoseResult();
        playerSound.Losing();
    }
    public void ReduceHealth()
    {
        health -= 1;

        float hurtUIAlpha = playerHurtUI.color.a;
        hurtUIAlpha = 2f;

        playerSound.PlayerHurting();
    }
    
    private void ThrowDagger()
    {

        anim.Play(ANIM_THROW);
        if (typer.currentTarget != null)
        {
            // Instantiate the dagger at the player's position
            currentDagger = Instantiate(daggerPrefab, throwPoint.position, Quaternion.identity);

            // Get the direction towards the enemy
            Vector3 direction = (typer.currentTarget.transform.position - throwPoint.position).normalized;

            // Set the velocity for the dagger
            Rigidbody daggerRb = currentDagger.GetComponent<Rigidbody>();
            if (daggerRb != null)
            {
                daggerRb.velocity = direction * 10f; // Adjust speed as needed
            }
        }
    }
}
