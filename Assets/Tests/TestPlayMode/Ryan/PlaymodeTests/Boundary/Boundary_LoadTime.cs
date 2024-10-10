using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Boundary_LoadTime : MonoBehaviour
{
    // Set the name of the scene you want to test loading
    private string sceneToLoad = "mainmenu";  // Replace with your actual scene name

    // Define the maximum allowable load time in seconds
    private float maxLoadTime = 5.0f;

    [UnityTest]
    public IEnumerator TestSceneLoadTime()
    {
        // Record the start time
        float startTime = Time.realtimeSinceStartup;

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the next frame
        }

        // Record the end time
        float loadTime = Time.realtimeSinceStartup - startTime;

        // Assert that the load time is within the acceptable limit
        Assert.LessOrEqual(loadTime, maxLoadTime, $"Scene load time exceeded {maxLoadTime} seconds. Actual load time: {loadTime} seconds.");
    }
}
