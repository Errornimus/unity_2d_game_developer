using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField]
    TextMeshProUGUI questionText;
    [SerializeField]
    QuestionSO question;

    [Header("Answers")]
    [SerializeField]
    GameObject[] answerButtons;
    bool hasAnsweredEarly;

    [Header("ButtonSprites")]
    [SerializeField]
    Sprite defaultAnswerSprite;
    [SerializeField]
    Sprite correctAnswerSprite;

    Timer timer;

    void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        GetNextQuestion();
    }

    void Update()
    {
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            setAnswerButtonsInteractableTo(false);
        }
    }

    private void DisplayQuestionAndAnswers()
    {
        questionText.text = question.Question;

        for (int i = 0; i < question.Answers.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.Answers[i];
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        setAnswerButtonsInteractableTo(false);
        DisplayAnswer(index);

        timer.cancelTimer();
    }

    private void DisplayAnswer(int index)
    {
        if (index == question.CorrectAnswerIndex)
        {
            questionText.text = "Correct Answer!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "Sorry, the correct answer was\n" + question.Answers[question.CorrectAnswerIndex];
            Image buttonImage = answerButtons[question.CorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        setAnswerButtonsBackToInitialState();
        DisplayQuestionAndAnswers();
        setAnswerButtonsInteractableTo(true);
    }

    private void setAnswerButtonsBackToInitialState()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    private void setAnswerButtonsInteractableTo(bool interactable)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = interactable;
        }
    }
}