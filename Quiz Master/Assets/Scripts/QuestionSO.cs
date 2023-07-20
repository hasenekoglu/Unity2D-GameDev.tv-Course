using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Questions", fileName = "Question")]

public class QuestionSO : ScriptableObject
{
  [TextArea(1, 10)]
  [SerializeField] string question = "Enter new question text here";
  [SerializeField] string[] answers = new string[5];
  [SerializeField] int correctAnswerIndex;

  public string GetQuestion()
  {
    return question;
  }


}
