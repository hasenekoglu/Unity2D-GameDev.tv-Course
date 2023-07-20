using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] QuestionSO question;
  [SerializeField] GameObject[] answerButtons;
  int correctAnswerIndex;

  [SerializeField] Sprite defaultAnswerSprite;
  [SerializeField] Sprite correctAnswerSprite;
  [SerializeField] Sprite inCorretAnswerSprite;

  void Start()
  {
    DisplayQuestion();
    GetNextQuestion();
  }


  public void OnAnswerSelected(int index)
  {
    Image correctButtonImage;
    Image inCorrectButtonImage;
    if (index == question.GetCorrectAnswerIndex())
    {
      questionText.text = "Correct";
      correctButtonImage = answerButtons[index].GetComponent<Image>();
      correctButtonImage.sprite = correctAnswerSprite;


    }
    else
    {

      string answerIndex = question.GetAnswer(index);
      inCorrectButtonImage = answerButtons[index].GetComponent<Image>();
      inCorrectButtonImage.sprite = inCorretAnswerSprite;
      string correctAnswer = question.GetAnswer(correctAnswerIndex);
      questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
      correctAnswerIndex = question.GetCorrectAnswerIndex();
      correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
      correctButtonImage.sprite = correctAnswerSprite;
      //    correctButtonImage = answerButtons[index].GetComponent<Image>();
      //    correctButtonImage.sprite = correctAnswerSprite;

    }
    SetButtonState(false);
  }
  void GetNextQuestion()
  {
    SetButtonState(true);
    SetDefaultButtonSprite();
    DisplayQuestion();

  }

  private void SetDefaultButtonSprite()
  {

    for (int i = 0; i < answerButtons.Length; i++)
    {
      Image defaultButtonImage = answerButtons[i].GetComponent<Image>();
      defaultButtonImage.sprite = defaultAnswerSprite;

    }

  }

  void DisplayQuestion()
  {
    questionText.text = question.GetQuestion();
    for (int i = 0; i < answerButtons.Length; i++)
    {
      TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
      buttonText.text = question.GetAnswer(i);
    }

  }
  void SetButtonState(bool state)
  {
    for (int i = 0; i < answerButtons.Length; i++)
    {
      Button button = answerButtons[i].GetComponent<Button>();
      button.interactable = state;
    }
  }
}
