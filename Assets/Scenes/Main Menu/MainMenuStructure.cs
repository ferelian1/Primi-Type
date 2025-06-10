using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenuStructure : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingPanel;
    public GameObject achievementPanel;
    public GameObject creditPanel;
    public GameObject leaderboardPanel;
    public GameObject loadingPanel;
    public PlayableDirector loadingTimeline;
    public AudioSource Musicaudio;

    [Header("Timeline Animation")]
    public PlayableDirector mainToSetting;
    public PlayableDirector settingToMain;

    void Start()
    {
        ShowMainMenuPanel();
    }

    private void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        achievementPanel.SetActive(false);
        creditPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
    }

    public void ShowMainMenuPanel()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void BackMainMenuPanel()
    {
        if (settingToMain != null)
            settingToMain.Play();

        // Pastikan Musicaudio dihidupkan lagi ketika kembali ke menu
        if (Musicaudio != null)
        {
            Musicaudio.Play();  // Mengaktifkan objek audio
        }
    }

    public void ShowSettingPanel()
    {
        HideAllPanels();
        settingPanel.SetActive(true);

        if (mainToSetting != null)
            mainToSetting.Play();
    }

    public void ShowAchievementPanel()
    {
        HideAllPanels();
        achievementPanel.SetActive(true);
    }

    public void ShowCreditPanel()
    {
        HideAllPanels();
        creditPanel.SetActive(true);
    }

    public void ShowLeaderboardPanel()
    {
        HideAllPanels();
        leaderboardPanel.SetActive(true);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadGameAsync("Level1"));
    }

    private IEnumerator LoadGameAsync(string sceneName)
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        if (loadingTimeline != null)
            loadingTimeline.Play();

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;

        while (asyncOp.progress < 0.9f)
            yield return null;

        if (loadingTimeline != null)
            loadingTimeline.Stop();
        Musicaudio.Stop();

        yield return null;

        asyncOp.allowSceneActivation = true;
    }



    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
