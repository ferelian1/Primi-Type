using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject pauseGroup;
    [SerializeField] private GameObject winGroup;
    [SerializeField] private GameObject loseGroup;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Typer typer;
    [SerializeField] private GameObject audioGroup;
    [SerializeField] private GameObject pausedAudioGroup;
    [SerializeField] private GameObject spawner;

    private AudioManager audios;
    private const string MAINMENU_SCENE = "MainMenu";

    private Enemy[] enemies;
    private bool isPaused;

    private void Start() {
        typer = FindObjectOfType<Typer>();
        audios = FindObjectOfType<AudioManager>();

    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused)
                Paused();
            else
                Resumed();
        }
    }

    public void Paused() {
        pauseGroup.SetActive(true);
        pausedAudioGroup.SetActive(true);

        HideGroupPanel();


        isPaused = true;
    }

    public void Resumed() {
        pauseGroup.SetActive(false);
        pausedAudioGroup.SetActive(false);

        ShowGroupPanel();

        isPaused = false;
    }

    public void Restarted() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quited() {

        SceneManager.LoadScene(MAINMENU_SCENE);
    }

    public void LeveledUp() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void WinResult() {
        winGroup.SetActive(true);
        HideGroupPanel();

        audios.Winning();
    }
    public void LoseResult() {
        loseGroup.SetActive(true);
        HideGroupPanel();

        audios.Losing();
    }

    private void ShowGroupPanel() {
        pauseButton.SetActive(true);
        typer.gameObject.SetActive(true);
        audioGroup.SetActive(true);
        pausedAudioGroup.SetActive(false);
        spawner.GetComponent<WaveSpawner>()?.ResumeSpawner();

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies) {
            GameObject enemyObject = enemy.gameObject;

            // Jika menggunakan NavMeshAgent, matikan pergerakan (tetapi tidak menonaktifkan GameObject)
            NavMeshAgent navAgent = enemyObject.GetComponent<NavMeshAgent>();
            if (navAgent != null) {
                navAgent.isStopped = false;  // Hentikan pergerakan NavMeshAgent
            }
        }
    }

    private void HideGroupPanel() {
        pauseButton.SetActive(false);
        audioGroup.SetActive(false);
        typer.gameObject.SetActive(false);
        spawner.GetComponent<WaveSpawner>()?.PauseSpawner();

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies) {
            GameObject enemyObject = enemy.gameObject;

            // Jika menggunakan NavMeshAgent, matikan pergerakan (tetapi tidak menonaktifkan GameObject)
            NavMeshAgent navAgent = enemyObject.GetComponent<NavMeshAgent>();
            if (navAgent != null) {
                navAgent.isStopped = true;  // Hentikan pergerakan NavMeshAgent
            }
        }
    }
}
