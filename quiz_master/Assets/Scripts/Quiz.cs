using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI questionText;

    [SerializeField]
    QuestionSO question;

    [SerializeField]
    GameObject[] answerButtons;

    void Start()
    {
        questionText.text = question.Question;

        for (int i = 0; i < question.Answers.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.Answers[i];
        }
    }
}
