using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
  [SerializeField] float timeToCompleteQuestion = 30f;
  [SerializeField] float timeToShowCorrectAnswer = 30f;
  public bool loadNextQuestion;
  public bool isAnswerQuestion = false;
  public float fillFraction;
  float timerValue;
  void Update()
  {
    UpdateTimer();
  }

  public void CancelTimer()
  {
    timerValue = 0;

  }
  void UpdateTimer()
  {
    timerValue -= Time.deltaTime;
    if (isAnswerQuestion)
    {
      if (timerValue > 0)
      {
        fillFraction = timerValue / timeToCompleteQuestion;
      }
      else
      {
        isAnswerQuestion = false;
        timerValue = timeToCompleteQuestion;
      }
    }
    else
    {

      if (timerValue > 0)
      {
        fillFraction = timerValue / timeToShowCorrectAnswer;
      }
      else
      {
        isAnswerQuestion = true;
        timerValue = timeToCompleteQuestion;
        loadNextQuestion = true;
      }
    }


    Debug.Log(isAnswerQuestion + ":" + timerValue + ":" + fillFraction);
  }
}
