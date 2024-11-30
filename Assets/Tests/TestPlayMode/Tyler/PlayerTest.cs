using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerSpawnerBoundaryTest
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
    public IEnumerator TestSpawnPlayer()
    {
        // Wait for the scene to load before starting the test
        while (!sceneLoaded)
        {
            yield return null; // Wait until the scene is fully loaded
        }

        // Cleanup any existing players
        //CleanupPlayer();

        // Create the player prefab
        GameObject playerPrefab = CreatePlayerPrefab();

        // Spawn one player
        Player spawnedPlayer = Spawn(playerPrefab, GetRandomSpawnPosition(spawnRadius), Quaternion.identity);
        
        yield return null; // Wait for one frame

        // Assert one player was spawned in addition to the one already in the scene
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        Assert.AreEqual(2, playerCount -1, "Expected one player to be spawned.");

        // Clean up
        //CleanupPlayer();
        Object.DestroyImmediate(playerPrefab);
    }
    */

    private GameObject CreatePlayerPrefab()
    {
        GameObject playerPrefab = new GameObject("Player");
        playerPrefab.GetComponent<Animator>();
        playerPrefab.GetComponent<Rigidbody2D>();
        playerPrefab.GetComponent<Collider2D>();
        playerPrefab.tag = "Player";
        Debug.Log("Player prefab created.");
        return playerPrefab;
    }

    private void CleanupPlayer()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Object.DestroyImmediate(player);
        }
    }

/*
    private Player Spawn(GameObject player, Vector3 position, Quaternion rotation)
    {
        // Instantiate the Slime prefab
        GameObject playerInstance = Object.Instantiate(player, position, rotation);
        return playerInstance.GetComponent<Player>(); // Return the Slime component from the instantiated object
    }
    */
}
