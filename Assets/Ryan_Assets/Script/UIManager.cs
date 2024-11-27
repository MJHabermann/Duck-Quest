using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputActionReference quitAction;  // Set this in Inspector
    [SerializeField] private InputActionReference restartAction;  // Set this in Inspector

  
    private void OnEnable()
    {
        quitAction.action.performed += OnQuitAction;
        restartAction.action.performed += OnRestartAction;

        quitAction.action.Enable();
        restartAction.action.Enable();
    }

    private void OnDisable()
    {
        quitAction.action.performed -= OnQuitAction;
        restartAction.action.performed -= OnRestartAction;

        quitAction.action.Disable();
        restartAction.action.Disable();
    }

    private void OnQuitAction(InputAction.CallbackContext context)
    {
        QuitGame();
    }

    private void OnRestartAction(InputAction.CallbackContext context)
    {
        RestartGame();
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
