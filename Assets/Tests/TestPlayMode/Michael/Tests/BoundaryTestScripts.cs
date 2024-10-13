using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BoundaryTestScripts
{
    // A Test behaves as an ordinary method
    private bool sceneLoaded = false;
    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'PlayerRoom'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("PlayerRoom", LoadSceneMode.Single);
    }
    private void SceneManagerSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene 'PlayerRoom' loaded.");
        sceneLoaded = true;
    }

    [UnityTest]
    public IEnumerator ExitObjectInScene()
    {
        GameObject groundObject = GameObject.FindWithTag("Exit");
        Assert.IsNotNull(groundObject);
        yield return null;
    }
}
