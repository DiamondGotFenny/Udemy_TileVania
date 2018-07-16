using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] float levelExitSlowMotion=0.2f;
    [SerializeField] float LevelLoadDelay = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = levelExitSlowMotion;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;

        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
        Destroy(scenePersist);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
