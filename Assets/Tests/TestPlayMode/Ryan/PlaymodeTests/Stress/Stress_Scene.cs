using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class Stress_Scene : MonoBehaviour
{
    public string mainMenuScene = "mainmenu";
    public string gameplayScene = "PlayerRoom";
    
    [UnityTest]
    public IEnumerator RapidSceneTransition()
    {
        int transitions = 30;  // Number of scene transitions

        for (int i = 0; i < transitions; i++)
        {
            // Load the main menu scene asynchronously
            AsyncOperation loadMainMenu = SceneManager.LoadSceneAsync(mainMenuScene);
            // Wait until the scene has fully loaded
            while (!loadMainMenu.isDone)
            {
                yield return null;  // Wait until the next frame
            }

            // Load the gameplay scene asynchronously
            AsyncOperation loadGameplay = SceneManager.LoadSceneAsync(gameplayScene);
            // Wait until the scene has fully loaded
            while (!loadGameplay.isDone)
            {
                yield return null;  // Wait until the next frame
            }
        }
    }
}
