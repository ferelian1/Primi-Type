using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private GameManager gameManager;

    private const string ANIM_THROW = "Throw";
    private const string ANIM_IDLE = "Throw";
    private const string ANIM_LOSE = "Throw";

    private void Awake()
    {
        
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
    }
    public void ReduceHealth()
    {
        health -= 1;
    }
}
