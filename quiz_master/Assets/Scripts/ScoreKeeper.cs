using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int correctAnswers { get; private set; } = 0;
    public int questionsSeen { get; private set; } = 0;

    public void increaseCorrectAnswers()
    {
        correctAnswers++;
    }

    public void increaseQuestionsSeen()
    {
        questionsSeen++;
    }

    public int calculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
