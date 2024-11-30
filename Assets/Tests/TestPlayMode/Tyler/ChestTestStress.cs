using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChestSpawnerStressTest
{
    private const float spawnRadius = 0.2f;
    private bool sceneLoaded = false;

    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'PlayerRoom'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("PlayerRoom", LoadSceneMode.Single);
    }

    private Vector3 GetRandomSpawnPosition(float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius; // Get a random point within a circle
        return new Vector3(randomPoint.x, 0, randomPoint.y); // Assuming y-axis is 0 for 2D plane
    }

    private void SceneManagerSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene 'PlayerRoom' loaded.");
        sceneLoaded = true;
    }

/*
    [UnityTest]
    public IEnumerator TestSpawnChest()
    {
        // Wait for the scene to load before starting the test
        while (!sceneLoaded)
        {
            yield return null; // Wait until the scene is fully loaded
        }

        // Cleanup any existing enemies
        CleanupChest();

        // Create the enemy prefab
        GameObject chestPrefab = CreateChestPrefab();
        int chestCount = 0;
        // Spawn one enemy
        while(true){
            Chest spawnedChest = Spawn(chestPrefab, GetRandomSpawnPosition(spawnRadius), Quaternion.identity);
            chestCount = GameObject.FindGameObjectsWithTag("Chest").Length;
            Debug.Log("Chest count: " + chestCount);
            yield return null;
            if(chestCount >= 1000){
                break;
            }
        }
        
        
        yield return null; // Wait for one frame

        // Assert one chest was spawned
        chestCount = GameObject.FindGameObjectsWithTag("Chest").Length;
        Assert.AreEqual(1000, chestCount, "Expected one chest to be spawned.");

        // Clean up
        CleanupChest();
        Object.DestroyImmediate(chestPrefab);
    }
    */

    private GameObject CreateChestPrefab()
    {
        GameObject chestPrefab = new GameObject("Chest");
        chestPrefab.AddComponent<Rigidbody2D>(); // Example component
        chestPrefab.tag = "Chest";
        Debug.Log("Chest prefab created.");
        return chestPrefab;
    }

    private void CleanupChest()
    {
        foreach (var chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            Object.DestroyImmediate(chest);
        }
    }
/*

    private Chest Spawn(GameObject chest, Vector3 position, Quaternion rotation)
    {
        // Instantiate the Slime prefab
        GameObject chestInstance = Object.Instantiate(chest, position, rotation);
        return chestInstance.GetComponent<Chest>(); // Return the Slime component from the instantiated object
    }
    */
}
