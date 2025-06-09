using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;  // ← untuk Timeline/PlayableDirector

public class MainMenuStructure : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingPanel;
    public GameObject achievementPanel;
    public GameObject creditPanel;
    public GameObject leaderboardPanel;

    [Header("Timeline Animation")]
    public PlayableDirector mainToSetting;  // diputar saat ShowSettingPanel
    public PlayableDirector settingToMain;  // diputar saat ShowMainMenuPanel

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
    }

    public void ShowSettingPanel()
    {
        HideAllPanels();
        settingPanel.SetActive(true);

        // Putar Timeline dari Main ke Setting
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
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
