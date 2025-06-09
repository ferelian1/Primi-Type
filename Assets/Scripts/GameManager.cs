using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseGroup;
    [SerializeField] private GameObject winGroup;
    [SerializeField] private GameObject loseGroup;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Typer typer;
    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject pausedAudioManager;
    [SerializeField] private GameObject spawner;

    private Enemy[] enemies;
    private bool isPaused;

    private void Start()
    {
        typer = FindObjectOfType<Typer>();


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }
        else if (isPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Resumed();
        }
    }

    public void Paused()
    {
        pauseGroup.SetActive(true);
        pauseButton.SetActive(false);
        typer.gameObject.SetActive(false);
        audioManager.SetActive(false);
        pausedAudioManager.SetActive(true);
        spawner.SetActive(false);

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            GameObject enemyObject = enemy.gameObject;

            // Jika menggunakan NavMeshAgent, matikan pergerakan (tetapi tidak menonaktifkan GameObject)
            NavMeshAgent navAgent = enemyObject.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.isStopped = true;  // Hentikan pergerakan NavMeshAgent
            }
        }
        isPaused = true;
    }

    public void Resumed()
    {
        pauseGroup.SetActive(false);
        pauseButton.SetActive(true);
        typer.gameObject.SetActive(true);
        audioManager.SetActive(true);
        pausedAudioManager.SetActive(false);
        spawner.SetActive(true);

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            GameObject enemyObject = enemy.gameObject;

            // Jika menggunakan NavMeshAgent, matikan pergerakan (tetapi tidak menonaktifkan GameObject)
            NavMeshAgent navAgent = enemyObject.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.isStopped = false;  // Hentikan pergerakan NavMeshAgent
            }
        }

        isPaused = false;
    }

    public void Restarted()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quited()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LeveledUp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void WinResult()
    {
        winGroup.SetActive(true);
        pauseButton.SetActive(false);
        typer.gameObject.SetActive(false);
        audioManager.SetActive(false);
        pausedAudioManager.SetActive(true);
        spawner.SetActive(false);
    }
    public void LoseResult()
    {
        loseGroup.SetActive(true);
        pauseButton.SetActive(false);
        typer.gameObject.SetActive(false);
        audioManager.SetActive(false);
        pausedAudioManager.SetActive(false);
        spawner.SetActive(false);

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            GameObject enemyObject = enemy.gameObject;

            // Jika menggunakan NavMeshAgent, matikan pergerakan (tetapi tidak menonaktifkan GameObject)
            NavMeshAgent navAgent = enemyObject.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.isStopped = false;  // Hentikan pergerakan NavMeshAgent
            }
        }

    }
}
