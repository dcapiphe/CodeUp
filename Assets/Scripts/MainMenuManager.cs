using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject languagePanel;

    [Header("Language Buttons")]
    public GameObject cPlusPlusButton;
    public GameObject pythonButton;
    public GameObject javaButton;

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        languagePanel.SetActive(true);
    }

    public void SelectCPlusPlus()
    {
        SceneManager.LoadScene("GameScene");
    }
}