using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    public void showFinalScore()
    {
        finalScoreText.text = "Congratulations!\nYou got a score of " + scoreKeeper.calculateScore() + "%";
    }

}
