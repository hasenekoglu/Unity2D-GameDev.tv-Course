using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    //[SerializeField] AudioClip checkPointSFX;


    void OnTriggerEnter2D(Collider2D other)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (other.tag == "Player")

        {

            // GetComponent<AudioSource>().PlayOneShot(checkPointSFX);
            // StartCoroutine(SceneDelay());
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(nextSceneIndex);

        }
        IEnumerator SceneDelay()
        {
            yield return new WaitForSecondsRealtime(.5f);
        }










        // StartCoroutine(LoadNextLevel()); ;
    }
    /* IEnumerator LoadNextLevel()
     {
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         yield return new WaitForSecondsRealtime(loadSceneTime);
         SceneManager.LoadScene(currentSceneIndex + 1);
     }*/
}
