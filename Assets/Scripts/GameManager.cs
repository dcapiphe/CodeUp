using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("C++ Question System")]
    public CppQuestionManager cppQuestionManager;

    [Header("Player and Enemy HP")]
    public int playerHP = 5;
    public int enemyHP = 5;

    [Header("UI")]
    public TMP_Text playerHPText;
    public TMP_Text enemyHPText;
    public TMP_Text outputText;
    public TMP_Text timerText;

    [Header("Game Buttons")]
    public Button attackButton;
    public Button previousButton;
    public Button nextButton;

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

        UpdateHPUI();
        UpdateTimerUI();
        UpdatePage();

        attackButton.onClick.AddListener(Attack);
        previousButton.onClick.AddListener(PreviousPage);
        nextButton.onClick.AddListener(NextPage);

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
        if (playerHP <= 0 || enemyHP <= 0)
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

    private void SelectSyntax(int index)
    {
        string selected = pages[currentPage][index];

        selectedAnswer = selected;

        cppQuestionManager.DisplaySelectedAnswer(selected);

        Debug.Log("Selected: " + selected);
    }

    private void Attack()
    {
        if (playerHP <= 0 || enemyHP <= 0)
            return;

        if (selectedAnswer == "")
        {
            Debug.Log("No syntax block selected!");
            return;
        }

        if (cppQuestionManager.CheckAnswer(selectedAnswer))
        {
            EnemyTakeDamage();

            if (enemyHP > 0)
            {
                cppQuestionManager.NextQuestion();
            }

            Debug.Log("Correct answer!");
        }
        else
        {
            PlayerTakeDamage();

            Debug.Log("Incorrect answer!");
        }

        selectedAnswer = "";

        cppQuestionManager.DisplayCurrentQuestion();

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
            Debug.Log("GAME OVER!");
        }
    }

    private void EnemyTakeDamage()
    {
        enemyHP--;

        if (enemyHP < 0)
            enemyHP = 0;

        UpdateHPUI();

        Debug.Log("Enemy HP: " + enemyHP);

        if (enemyHP == 0)
        {
            Debug.Log("YOU WIN!");
        }
    }

    private void UpdateHPUI()
    {
        playerHPText.text = "HP: " + playerHP;
        enemyHPText.text = "HP: " + enemyHP;
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
}