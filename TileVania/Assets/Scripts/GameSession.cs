using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] TextMeshProUGUI scoreText;
    float coin;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        liveText.text = playerLives.ToString();
        scoreText.text = score.ToString();






    }
    public void ProcessPlayerDeath()
    {


        if (playerLives > 1)

        {
            StartCoroutine(TakeLife());



        }
        else
        {


            StartCoroutine(ResetGameSession());

        }
    }
    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }


    IEnumerator TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(currentSceneIndex);
        liveText.text = playerLives.ToString();

    }

    IEnumerator ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(0);

        Destroy(gameObject);

    }


}
