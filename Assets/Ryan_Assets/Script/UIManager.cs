using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("mainmenu");
        Debug.Log("Scene restarted");
    }
}
