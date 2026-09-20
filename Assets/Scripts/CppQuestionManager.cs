using UnityEngine;
using TMPro;

public class CppQuestionManager : MonoBehaviour
{
    [Header("C++ Questions")]
    public QuestionData[] questions;

    [Header("Question UI")]
    public TMP_Text codeText;
    public TMP_Text missingSlotText;
    public TMP_Text outputText;

    private int currentQuestionIndex = 0;

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

        codeText.text = question.codeBefore + " ??? " + question.codeAfter;

        missingSlotText.text = "???";

        outputText.text = "Output: " + question.output;
    }

    public void DisplaySelectedAnswer(string selectedAnswer)
    {
        QuestionData question = GetCurrentQuestion();

        if (question == null)
            return;

        codeText.text = question.codeBefore + " " + selectedAnswer + " " + question.codeAfter;

        missingSlotText.text = selectedAnswer;
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