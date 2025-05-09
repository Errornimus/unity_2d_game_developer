using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly = true;

    [Header("ButtonSprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete { get; private set; } = false;

    void Awake()
    {
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Start()
    {
        scoreText.text = "";
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

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
        questionText.text = currentQuestion.Question;

        for (int i = 0; i < currentQuestion.Answers.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.Answers[i];
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        setAnswerButtonsInteractableTo(false);
        DisplayAnswer(index);
        timer.cancelTimer();
        scoreText.text = "Score: " + scoreKeeper.calculateScore().ToString() + "%";
    }

    private void DisplayAnswer(int index)
    {
        if (index == currentQuestion.CorrectAnswerIndex)
        {
            questionText.text = "Correct Answer!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.increaseCorrectAnswers();
        }
        else
        {
            questionText.text = "Sorry, the correct answer was\n" + currentQuestion.Answers[currentQuestion.CorrectAnswerIndex];
            Image buttonImage = answerButtons[currentQuestion.CorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            setAnswerButtonsBackToInitialState();
            GetRandomQuestion();
            DisplayQuestionAndAnswers();
            setAnswerButtonsInteractableTo(true);
            progressBar.value++;
            scoreKeeper.increaseQuestionsSeen();
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);
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