using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
    public void PlayGame () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
    public void QuitGame () {
        Application.Quit ();
    }

    public void BackToMainMenu () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
        ScoreScript.scoreValue = 0;
    }

    public void DoneButton () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

    }

    public void RestartButton () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 2);

    }
}