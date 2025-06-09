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

    private const string ANIM_THROW = "isThrowing";
    private const string ANIM_LOSE = "isLosing";

    private AudioManager playerSound;
    private GameObject currentDagger;
    private readonly Quaternion DEFAULT_ROTATION = Quaternion.Euler(0, 0, 0); // arah depan

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

        if (playerHurtUI != null)
        {
            Color startColor = playerHurtUI.color;
            startColor.a -= 0.05f;
            playerHurtUI.color = startColor;
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
        anim.SetBool(ANIM_LOSE, true);
        gameManager.LoseResult();
        playerSound.Losing();
    }
    public void ReduceHealth()
    {
        health -= 1;

        if (playerHurtUI != null)
        {
            Color hurtColor = playerHurtUI.color;
            hurtColor.a = .4f;
            playerHurtUI.color = hurtColor;
        }

        playerSound.PlayerHurting();
    }

    public void ThrowDagger(Enemy target)
    {

        anim.SetBool(ANIM_THROW, true);

        StartCoroutine(ResetThrowAnim());
        
        FaceEnemy(target.transform.position);


        if (daggerPrefab != null && throwPoint != null)
        {
            GameObject daggerObj = Instantiate(daggerPrefab, throwPoint.position, Quaternion.identity);
            Dagger dagger = daggerObj.GetComponent<Dagger>();
            if (dagger != null)
            {
                dagger.SetTarget(target);


            }
        }
    }

    private IEnumerator ResetThrowAnim()
    {
        yield return new WaitForSeconds(0.5f); // sesuaikan durasi animasi lempar kamu
        anim.SetBool(ANIM_THROW, false);
        transform.rotation = DEFAULT_ROTATION;
    }

    private void FaceEnemy(Vector3 enemyPosition)
    {
        Vector3 direction = (enemyPosition - transform.position).normalized;
        direction.y = 0f; // supaya tidak condong ke atas/bawah

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
    }

}

