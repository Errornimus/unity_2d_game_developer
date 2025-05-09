using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float timeToCompleteQuestion = 30.0f;
    [SerializeField]
    float timeToShowCorrectAnswer = 10.0f;

    public bool loadNextQuestion { get; set; }
    public bool isAnsweringQuestion { get; set; } = false;

    float fillFraction;
    float timerValue;

    void Update()
    {
        updateTimer();
    }

    void updateTimer()
    {
        timerValue -= Time.deltaTime;

        if (timerValue > 0)
        {
            float timeOfCurrentState = isAnsweringQuestion ? timeToCompleteQuestion : timeToShowCorrectAnswer;

            fillFraction = timerValue / timeOfCurrentState;
            GetComponentInChildren<Image>().fillAmount = fillFraction;
        }
        else
        {
            isAnsweringQuestion = !isAnsweringQuestion;

            if (isAnsweringQuestion)
            {
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
            else
                timerValue = timeToShowCorrectAnswer;
        }
    }

    public void cancelTimer()
    {
        timerValue = 0;
    }
}
