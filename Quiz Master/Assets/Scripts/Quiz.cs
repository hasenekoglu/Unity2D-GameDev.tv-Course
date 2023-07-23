using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
  [Header("Questions")]
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
  [SerializeField] QuestionSO currentQuestion;

  [Header("Answers")]
  [SerializeField] GameObject[] answerButtons;

  int correctAnswerIndex;
  bool hasAnsweredEarly;
  [Header("Button Colors")]
  [SerializeField] Sprite defaultAnswerSprite;
  [SerializeField] Sprite correctAnswerSprite;
  [SerializeField] Sprite inCorretAnswerSprite;
  [Header("Timer")]
  [SerializeField] Image timerImage;

  Timer timer;

  void Start()
  {
    timer = FindObjectOfType<Timer>();

  }
  void Update()
  {
    timerImage.fillAmount = timer.fillFraction;
    if (timer.loadNextQuestion)
    {
      GetNextQuestion();
      timer.loadNextQuestion = false;
    }
    else if (!hasAnsweredEarly && !timer.isAnswerQuestion)
    {
      hasAnsweredEarly = false;
      DisplayAnswer(-1);
      SetButtonState(false);
    }
  }


  public void OnAnswerSelected(int index)
  {
    hasAnsweredEarly = true;
    DisplayAnswer(index);

    SetButtonState(false);
    timer.CancelTimer();
  }
  void DisplayAnswer(int index)
  {
    Image correctButtonImage;
    Image inCorrectButtonImage;
    if (index == currentQuestion.GetCorrectAnswerIndex())
    {
      questionText.text = "Correct";
      correctButtonImage = answerButtons[index].GetComponent<Image>();
      correctButtonImage.sprite = correctAnswerSprite;


    }
    else
    {

      string answerIndex = currentQuestion.GetAnswer(index);
      inCorrectButtonImage = answerButtons[index].GetComponent<Image>();
      inCorrectButtonImage.sprite = inCorretAnswerSprite;
      string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
      questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
      correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
      correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
      correctButtonImage.sprite = correctAnswerSprite;
      //    correctButtonImage = answerButtons[index].GetComponent<Image>();
      //    correctButtonImage.sprite = correctAnswerSprite;

    }
  }
  void GetNextQuestion()
  {
    if (questions.Count > 0)
    {
      SetButtonState(true);
      SetDefaultButtonSprite();
      GetRandomQuestion();
      DisplayQuestion();
    }


  }

  private void GetRandomQuestion()
  {
    int index = Random.Range(0, questions.Count);
    currentQuestion = questions[index];
    if (questions.Contains(currentQuestion))
    {
      questions.Remove(currentQuestion);
    }
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
    questionText.text = currentQuestion.GetQuestion();
    for (int i = 0; i < answerButtons.Length; i++)
    {
      TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
      buttonText.text = currentQuestion.GetAnswer(i);
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
