using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class Stress_Scene: MonoBehaviour
{
    public string mainMenuScene = "mainmenu";
    public string gameplayScene = "PlayerRoom";
    
    [UnityTest]
    public IEnumerator RapidSceneTransition()
    {
        int transitions = 10;  // Number of scene transitions

        for (int i = 0; i < transitions; i++)
        {
            SceneManager.LoadScene(mainMenuScene);
            yield return new WaitForSeconds(0.1f);  // Wait for scene load
            SceneManager.LoadScene(gameplayScene);
            yield return new WaitForSeconds(0.1f);  // Wait for scene load
        }
    }
}
