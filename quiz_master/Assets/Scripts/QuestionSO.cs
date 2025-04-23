using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField]
    string question = "Enter new question text here";
    public string Question { get => question; }

    [field: SerializeField]
    // string[] answers = new string[4];
    public string[] Answers { get; private set; } = new string[4];

    [field: SerializeField]
    public int CorrectAnswerIndex { get; private set; }
}