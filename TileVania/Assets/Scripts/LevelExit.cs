using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] float loadSceneTime = 0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

        // StartCoroutine(LoadNextLevel()); ;
    }
    /* IEnumerator LoadNextLevel()
     {
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         yield return new WaitForSecondsRealtime(loadSceneTime);
         SceneManager.LoadScene(currentSceneIndex + 1);
     }*/
}
