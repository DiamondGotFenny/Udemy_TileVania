using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour {

    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReLoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
