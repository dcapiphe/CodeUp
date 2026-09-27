using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("C++ Question System")]
    public CppQuestionManager cppQuestionManager;

    [Header("Player HP")]
    public int playerHP = 5;

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public Button gameOverBackToMenuButton;

    [Header("Enemies")]
    public Enemy[] enemies;

    private int currentEnemyIndex = 0;

    private Enemy currentEnemy;

    [Header("UI")]
    public TMP_Text playerHPText;
    public TMP_Text enemyHPText;
    public TMP_Text outputText;
    public TMP_Text timerText;

    [Header("Game Buttons")]
    public Button attackButton;
    public Button previousButton;
    public Button nextButton;
    public Button pauseButton;
    public Button hintButton;

    [Header("Pause UI")]
    public GameObject pausePanel;
    public Button resumeButton;
    public Button backToMenuButton;

    [Header("Hint System")]
    public int maxHints = 5;
    private int hintsRemaining;
    private bool hintUsedForCurrentQuestion = false;

    public TMP_Text hintText;

    [Header("Syntax Buttons")]
    public Button[] syntaxButtons;
    public TMP_Text[] syntaxButtonTexts;

    [Header("Timer")]
    public float timeLimit = 15f;

    private float currentTime;

    private string selectedAnswer = "";

    private int currentPage = 0;

    private string[][] pages =
    {
        // PAGE 1 - Keywords
        new string[]
        {
            "cout",
            "cin",
            "int",
            "char",
            "bool",
            "void",
            "if",
            "else",
            "for"
        },

        // PAGE 2 - Operators
        new string[]
        {
            "=",
            "==",
            "!=",
            "+",
            "-",
            "*",
            "/",
            "<",
            ">"
        },

        // PAGE 3 - Syntax
        new string[]
        {
            ";",
            "(",
            ")",
            "{",
            "}",
            "\"",
            "<<",
            ">>",
            "endl"
        }
    };

    private void Start()
    {
        currentTime = timeLimit;

        hintsRemaining = maxHints;

        SetupEnemies();

        UpdateHPUI();
        UpdateTimerUI();
        UpdatePage();
        UpdateHintUI();
        UpdateScoreUI();

        attackButton.onClick.AddListener(Attack);
        previousButton.onClick.AddListener(PreviousPage);
        nextButton.onClick.AddListener(NextPage);

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        backToMenuButton.onClick.AddListener(BackToMenu);
        hintButton.onClick.AddListener(UseHint);
        gameOverBackToMenuButton.onClick.AddListener(BackToMenu);


        for (int i = 0; i < syntaxButtons.Length; i++)
        {
            int buttonIndex = i;

            syntaxButtons[i].onClick.AddListener(() =>
            {
                SelectSyntax(buttonIndex);
            });
        }
    }

    private void Update()
    {
        if (playerHP <= 0 || currentEnemy == null || currentEnemy.IsDead)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = timeLimit;

            PlayerTakeDamage();

            Debug.Log("Time ran out!");
        }

        UpdateTimerUI();
    }

    private void SetupEnemies()
    {
        if (enemies == null || enemies.Length == 0)
        {
            Debug.LogError("No enemies have been assigned to GameManager!");
            return;
        }

        currentEnemyIndex = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
                continue;

            enemies[i].ResetEnemy();

            if (i == 0)
            {
                enemies[i].gameObject.SetActive(true);
            }
            else
            {
                enemies[i].gameObject.SetActive(false);
            }
        }

        currentEnemy = enemies[0];

        Debug.Log("Enemy 1 started with " + currentEnemy.CurrentHP + " HP.");
    }

    private void SelectSyntax(int index)
    {
        string selected = pages[currentPage][index];

        selectedAnswer = selected;

        cppQuestionManager.DisplaySelectedAnswer(selected);

        Debug.Log("Selected: " + selected);
    }

    private void Attack()
    {
        if (playerHP <= 0 || currentEnemy == null || currentEnemy.IsDead)
            return;

        if (selectedAnswer == "")
        {
            Debug.Log("No syntax block selected!");
            return;
        }

        if (cppQuestionManager.CheckAnswer(selectedAnswer))
        {
            AddScore(20);

            EnemyTakeDamage();

            if (currentEnemy != null && !currentEnemy.IsDead)
            {
                cppQuestionManager.NextQuestion();
            }

            Debug.Log("Correct answer! +20 points");
        }

        else
        {
            PlayerTakeDamage();

            Debug.Log("Incorrect answer!");
        }

        selectedAnswer = "";

        cppQuestionManager.DisplayCurrentQuestion();

        hintUsedForCurrentQuestion = false;
        UpdateHintUI();

        currentTime = timeLimit;
    }

    private void PlayerTakeDamage()
    {
        playerHP--;

        if (playerHP < 0)
            playerHP = 0;

        UpdateHPUI();

        Debug.Log("Player HP: " + playerHP);

        if (playerHP == 0)
        {
            ShowGameOver();
        }
    }

    private void EnemyTakeDamage()
    {
        if (currentEnemy == null)
            return;

        currentEnemy.TakeDamage();

        UpdateHPUI();

        if (currentEnemy.IsDead)
        {
            Debug.Log(currentEnemy.name + " defeated!");

            MoveToNextEnemy();
        }
    }

    private void MoveToNextEnemy()
    {
        currentEnemyIndex++;

        if (currentEnemyIndex >= enemies.Length)
        {
            Debug.Log("YOU WIN! All 8 enemies defeated!");
            currentEnemy = null;
            UpdateHPUI();
            return;
        }

        currentEnemy = enemies[currentEnemyIndex];

        currentEnemy.ResetEnemy();

        Debug.Log(
            "Enemy " +
            (currentEnemyIndex + 1) +
            " appeared with " +
            currentEnemy.CurrentHP +
            " HP."
        );

        UpdateHPUI();
    }

    private void UpdateHPUI()
    {
        playerHPText.text = "HP: " + playerHP;

        if (currentEnemy != null)
        {
            enemyHPText.text = "HP: " + currentEnemy.CurrentHP;
        }
        else
        {
            enemyHPText.text = "HP: 0";
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    private void UpdatePage()
    {
        for (int i = 0; i < syntaxButtons.Length; i++)
        {
            syntaxButtonTexts[i].text = pages[currentPage][i];
        }
    }

    private void PreviousPage()
    {
        currentPage--;

        if (currentPage < 0)
            currentPage = pages.Length - 1;

        UpdatePage();

        selectedAnswer = "";

        cppQuestionManager.DisplayCurrentQuestion();

        hintUsedForCurrentQuestion = false;
        UpdateHintUI();
    }

    private void NextPage()
    {
        currentPage++;

        if (currentPage >= pages.Length)
            currentPage = 0;

        UpdatePage();

        selectedAnswer = "";

        cppQuestionManager.DisplayCurrentQuestion();
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("Game Paused");
    }

    private void ResumeGame()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        Debug.Log("Game Resumed");
    }

    private void BackToMenu()
    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void UseHint()
    {
        if (hintsRemaining <= 0)
        {
            Debug.Log("No hints remaining!");
            return;
        }

        if (hintUsedForCurrentQuestion)
        {
            Debug.Log("Hint already used for this question!");
            return;
        }

        if (playerHP <= 0 || currentEnemy == null || currentEnemy.IsDead)
            return;

        string correctAnswer = cppQuestionManager.RevealHint();

        if (string.IsNullOrEmpty(correctAnswer))
            return;

        selectedAnswer = correctAnswer;

        hintsRemaining--;

        hintUsedForCurrentQuestion = true;

        UpdateHintUI();

        Debug.Log("Hint used! Correct answer: " + correctAnswer);
    }

    private void UpdateHintUI()
    {
        if (hintText != null)
        {
            hintText.text = hintsRemaining.ToString();
        }

        if (hintButton != null)
        {
            hintButton.interactable =
                hintsRemaining > 0 && !hintUsedForCurrentQuestion;
        }
    }

    private void AddScore(int points)
    {
        score += points;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void ShowGameOver()
    {
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = score.ToString();
        }

        Debug.Log("GAME OVER! Final Score: " + score);
    }


}