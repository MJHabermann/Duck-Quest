using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BoundaryTestScripts
{
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
        //sceneLoaded = true;
    }

    [UnityTest]
    public IEnumerator ExitObjectInScene()
    {
        GameObject exitObject = GameObject.FindWithTag("Exit");
        Assert.IsNotNull(exitObject);
        yield return null;
    }
    [UnityTest]
    public IEnumerator GroundInScene()
    {
        GameObject groundObject = GameObject.FindWithTag("Ground");
        Assert.IsNotNull(groundObject);
        yield return null;
    }
    [UnityTest]
    public IEnumerator WallIsLoaded()
    {
        GameObject collisionObject = GameObject.FindWithTag("Wall");
        Assert.IsNotNull(collisionObject);
        yield return null;
    }
    [UnityTest]
    public IEnumerator LoadScenesWithDelay()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            SceneManager.LoadScene(sceneName);
            yield return null;
            yield return new WaitForSeconds(1f);
        }
    }
}
