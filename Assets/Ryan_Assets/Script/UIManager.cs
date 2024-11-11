using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Duck_Quest gameControls;

    private void Awake()
    {
        gameControls = new Duck_Quest();
    }

    public void OnClick(string action)
    {
        if (action == "Quit")
        {
            QuitGame();
        }
        else if (action == "Restart")
        {
            RestartGame();
        }
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("mainmenu");
        Debug.Log("Scene restarted");
    }
}
