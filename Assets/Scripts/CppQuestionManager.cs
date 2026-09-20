using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CppQuestionManager : MonoBehaviour
{
    [Header("C++ Questions")]
    public QuestionData[] questions;

    [Header("Question UI")]
    public TMP_Text codeText;
    public Image missingBlock;
    public TMP_Text missingAnswerText;
    public TMP_Text outputText;

    [Header("Missing Block Size")]
    public float blockWidth = 100f;
    public float blockHeight = 50f;

    [Header("Answer Color")]
    public Color correctAnswerColor = Color.green;

    [Header("Missing Slot Color")]
    public Color missingSlotColor = Color.yellow;

    private int currentQuestionIndex = 0;

    // Saved position of the missing block
    private Vector3 missingBlockPosition;

    private bool blockPositionSaved = false;

    private void Start()
    {
        DisplayCurrentQuestion();
    }

    public QuestionData GetCurrentQuestion()
    {
        if (questions == null || questions.Length == 0)
        {
            Debug.LogWarning("No C++ questions have been added.");
            return null;
        }

        return questions[currentQuestionIndex];
    }

    public bool CheckAnswer(string selectedAnswer)
    {
        QuestionData question = GetCurrentQuestion();

        if (question == null)
            return false;

        return selectedAnswer == question.correctAnswer;
    }

    public void DisplayCurrentQuestion()
    {
        QuestionData question = GetCurrentQuestion();

        if (question == null)
            return;

        blockPositionSaved = false;

        // CodeText ALWAYS keeps the same ??? placeholder.
        // This prevents the code spacing from changing.
        string invisibleMissingSlot =
            "<color=#FFFFFF00>???</color>";

        codeText.text =
            question.codeBefore +
            " " +
            invisibleMissingSlot +
            " " +
            question.codeAfter;

        outputText.text =
            "Output: " + question.output;

        // Display ??? inside the Block
        string missingColorHex =
            ColorUtility.ToHtmlStringRGB(missingSlotColor);

        missingAnswerText.text =
            "<color=#" +
            missingColorHex +
            ">???</color>";

        codeText.ForceMeshUpdate();

        SaveMissingBlockPosition();
    }

    public void DisplaySelectedAnswer(string selectedAnswer)
    {
        QuestionData question = GetCurrentQuestion();

        if (question == null)
            return;

        string colorHex =
            ColorUtility.ToHtmlStringRGB(correctAnswerColor);

        missingAnswerText.text =
            "<color=#" +
            colorHex +
            ">" +
            selectedAnswer +
            "</color>";

        if (missingBlock != null && blockPositionSaved)
        {
            missingBlock.transform.position =
                missingBlockPosition;
        }
    }

    private void SaveMissingBlockPosition()
    {
        if (missingBlock == null || codeText == null)
            return;

        codeText.ForceMeshUpdate();

        TMP_TextInfo textInfo = codeText.textInfo;

        int questionMarkIndex = -1;

        for (int i = 0; i < textInfo.characterCount - 2; i++)
        {
            if (textInfo.characterInfo[i].character == '?' &&
                textInfo.characterInfo[i + 1].character == '?' &&
                textInfo.characterInfo[i + 2].character == '?')
            {
                questionMarkIndex = i;
                break;
            }
        }

        if (questionMarkIndex == -1)
        {
            Debug.LogWarning("Could not find ??? in CodeText.");
            return;
        }

        TMP_CharacterInfo firstCharacter =
            textInfo.characterInfo[questionMarkIndex];

        TMP_CharacterInfo lastCharacter =
            textInfo.characterInfo[questionMarkIndex + 2];

        Vector3 center =
            (firstCharacter.bottomLeft +
             lastCharacter.topRight) / 2f;

        missingBlockPosition =
            codeText.transform.TransformPoint(center);

        blockPositionSaved = true;

        missingBlock.transform.position =
            missingBlockPosition;

        RectTransform blockRect =
            missingBlock.GetComponent<RectTransform>();

        blockRect.sizeDelta =
            new Vector2(blockWidth, blockHeight);
    }

    public void NextQuestion()
    {
        if (questions == null || questions.Length == 0)
            return;

        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            currentQuestionIndex = 0;
        }

        DisplayCurrentQuestion();
    }

    public void ResetQuestions()
    {
        currentQuestionIndex = 0;

        DisplayCurrentQuestion();
    }
}