using UnityEngine;

public class CppQuestionManager : MonoBehaviour
{
    [Header("C++ Questions")]
    public QuestionData[] questions;

    private int currentQuestionIndex = 0;

    public QuestionData GetCurrentQuestion()
    {
        if (questions == null || questions.Length == 0)
        {
            Debug.LogWarning("No C++ questions have been added.");
            return null;
        }

        return questions[currentQuestionIndex];
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
    }

    public void ResetQuestions()
    {
        currentQuestionIndex = 0;
    }
}