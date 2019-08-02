using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
    public void PlayGame () {
        SceneManager.LoadScene (1);
    }
    public void QuitGame () {
        Application.Quit ();
    }

    public void BackToMainMenu () {
        SceneManager.LoadScene (0);
        ScoreScript.scoreValue = 0;
    }

    public void DoneButton () {
        SceneManager.LoadScene (2);

    }

    public void RestartButton () {
        SceneManager.LoadScene(0);

    }
}