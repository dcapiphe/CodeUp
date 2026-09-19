using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject languagePanel;
    public GameObject leaderboardPanel;
    public GameObject creditsPanel;

    [Header("Language Buttons")]
    public GameObject cPlusPlusButton;
    public GameObject pythonButton;
    public GameObject javaButton;

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        languagePanel.SetActive(true);
    }

    public void Leaderboard()
    {
        mainMenuPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }

    public void Credits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    // Back button for Language Panel
    public void BackFromLanguage()
    {
        languagePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Back button for Leaderboard Panel
    public void BackFromLeaderboard()
    {
        leaderboardPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Back button for Credits Panel
    public void BackFromCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SelectCPlusPlus()
    {
        SceneManager.LoadScene("GameScene");
    }
}