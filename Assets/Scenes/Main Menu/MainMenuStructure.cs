using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStructure : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingPanel;
    public GameObject achievementPanel;
    public GameObject creditPanel;
    public GameObject leaderboardPanel;

    void Start()
    {
        ShowMainMenuPanel();
    }


    private void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        settingPanel.SetActive(false);
        achievementPanel.SetActive(false);
        creditPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
    }

    public void ShowMainMenuPanel()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }


    public void ShowSettingPanel()
    {
        HideAllPanels();
        settingPanel.SetActive(true);
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

    public void LeaderboardPanel()
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