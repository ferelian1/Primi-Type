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
    [SerializeField] private GameObject audioGroup;
    [SerializeField] private GameObject pausedAudioGroup;
    [SerializeField] private GameObject spawner;

    private AudioManager audios;

    private Enemy[] enemies;
    private bool isPaused;

    private void Start()
    {
        typer = FindObjectOfType<Typer>();
        audios = FindObjectOfType<AudioManager>();

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Paused();
            else
                Resumed();
        }
    }

    public void Paused()
    {
        pauseGroup.SetActive(true);
        pauseButton.SetActive(false);
        audioGroup.SetActive(false);
        pausedAudioGroup.SetActive(true);
        typer.gameObject.SetActive(false);
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
        audioGroup.SetActive(true);
        pausedAudioGroup.SetActive(false);
        spawner.SetActive(true);
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            GameObject enemyObject = enemy.gameObject;

            // Jika menggunakan NavMeshAgent, matikan pergerakan (tetapi tidak menonaktifkan GameObject)
            NavMeshAgent navAgent = enemyObject.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.isStopped = false;  // Hentikan semua pergerakan NavMeshAgent
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

        SceneManager.LoadScene("Main Menu");
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
        audioGroup.SetActive(false);
        spawner.SetActive(false);

        audios.Winning();
    }
    public void LoseResult()
    {
        loseGroup.SetActive(true);
        pauseButton.SetActive(false);
        typer.gameObject.SetActive(false);
        audioGroup.SetActive(false);
        spawner.SetActive(false);

        audios.Losing();

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            GameObject enemyObject = enemy.gameObject;

            // Jika menggunakan NavMeshAgent, matikan pergerakan (tetapi tidak menonaktifkan GameObject)
            NavMeshAgent navAgent = enemyObject.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.isStopped = true;  // Hentikan semua pergerakan NavMeshAgent
            }
        }

    }
}
