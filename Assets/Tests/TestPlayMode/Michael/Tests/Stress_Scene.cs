using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using System.Runtime.CompilerServices;

public class Stress_GrassGrids : MonoBehaviour
{
    public int maxGridSize = 1000;
    public float spacing = 1.0f;
    public float spawnDelay = 0.25f;
    private float lastSpawnTime;
    private bool sceneLoaded = false;
    private GameObject grassPrefab;
    private List<GameObject> grassObjects = new List<GameObject>();
    GameObject drops;
    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'Town'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("Town", LoadSceneMode.Single);
    }

    private void SceneManagerSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene 'Town' loaded.");
        sceneLoaded = true;
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        while (!sceneLoaded)
        {
            yield return null;
        }

        grassPrefab = FindGrassPrefabInScene();
        drops = GameObject.Find("Heart");
        if (grassPrefab != null)
        {
            Debug.Log("Grass prefab found successfully.");
        }
        else
        {
            Debug.LogError("Grass prefab could not be found in the scene!");
            Assert.Fail("Failed to find the grass prefab.");
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator StressGrassGrid()
    {
        Debug.Log($"Starting grass grid test, spawning grass grid of size {maxGridSize}...");
        for (int i = 1; i <= maxGridSize; i++)
        {
            SpawnGrassGrid(i);
            yield return new WaitForSeconds(spawnDelay);
            ClearGrass();
            yield return new WaitForSeconds(0.1f); // Give Unity time to destroy objects
        }
        yield return null;
    }

    private void SpawnGrassGrid(int gridSize)
    {
        if (grassPrefab == null)
        {
            Debug.LogError("Grass prefab is not assigned!");
            return;
        }

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing,  y * spacing,0);
                GameObject grassBlock = Instantiate(grassPrefab, spawnPosition, Quaternion.identity);
                grassObjects.Add(grassBlock);
            }
        }
        Debug.Log("Spawned grid of size: " + gridSize + "x" + gridSize);
    }

    private void ClearGrass()
    {
        foreach (GameObject grass in grassObjects)
        {
            if (grass != null)
            {
                Destroy(grass);
            }
        }
        grassObjects.Clear(); // Clear the list after destruction
        Debug.Log("Cleared grass blocks");
    }

    private GameObject FindGrassPrefabInScene()
    {
        GameObject grassPrefab = GameObject.Find("GrassBlock");

        if (grassPrefab == null)
        {
            Debug.LogError("Grass Prefab not found in the scene!");
        }

        return grassPrefab;
    }
}
/*
public IEnumerator RapidSceneTransition()
{
    int i = 0;
    float transitions = 10.0f;
    while(true)
    {
        SceneManager.LoadScene("PlayerRoom");
        yield return new WaitForSeconds((float)(1.0 - i/transitions));
        SceneManager.LoadScene("Town");
        yield return new WaitForSeconds((float)(1.0 - i/transitions));
        Debug.Log("Loading Iteration:" + i + " Waited " + ((float)(1.0 - i/ transitions)) + " inbetween Transitions");
        i++;
        yield return null;
        if (i > transitions)
        {
            break;
        }
    }
}*/

