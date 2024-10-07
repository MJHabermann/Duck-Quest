using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemySpawnerBoundaryTest
{
    private const int maxEnemies = 400;  // Upper boundary for testing
    private const float spawnRadius = 0.2f; // Area to spawn enemies
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

    [UnityTest]
    public IEnumerator TestSpawnZeroEnemies()
    {
        // Wait for the scene to load before starting the test
        while (!sceneLoaded)
        {
            yield return null; // Wait until the scene is fully loaded
        }

        // Cleanup any existing enemies
        CleanupEnemies();

        // Create the enemy prefab
        GameObject enemyPrefab = CreateEnemyPrefab();

        // No enemies to spawn
        Debug.Log("Spawn zero enemies.");
        
        // Simply yield to simulate no spawning
        yield return null;  // Wait for one frame

        // Assert no enemies were spawned
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Assert.AreEqual(0, enemyCount -1, "Expected no enemies to be spawned.");

        // Clean up
        Object.DestroyImmediate(enemyPrefab);
    }

    [UnityTest]
    public IEnumerator TestSpawnOneEnemy()
    {
        // Wait for the scene to load before starting the test
        while (!sceneLoaded)
        {
            yield return null; // Wait until the scene is fully loaded
        }

        // Cleanup any existing enemies
        CleanupEnemies();

        // Create the enemy prefab
        GameObject enemyPrefab = CreateEnemyPrefab();

        // Spawn one enemy
        Slime spawnedSlime = Spawn(enemyPrefab, GetRandomSpawnPosition(spawnRadius), Quaternion.identity);
        
        yield return null; // Wait for one frame

        // Assert one enemy was spawned
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Assert.AreEqual(1, enemyCount -1, "Expected one enemy to be spawned.");

        // Clean up
        CleanupEnemies();
        Object.DestroyImmediate(enemyPrefab);
    }

    [UnityTest]
    public IEnumerator TestSpawnMaxEnemies()
    {
        // Wait for the scene to load before starting the test
        while (!sceneLoaded)
        {
            yield return null; // Wait until the scene is fully loaded
        }

        // Cleanup any existing enemies
        CleanupEnemies();

        // Create the enemy prefab
        GameObject enemyPrefab = CreateEnemyPrefab();

        for (int i = 0; i < maxEnemies; i++)
        {
            Spawn(enemyPrefab, GetRandomSpawnPosition(spawnRadius), Quaternion.identity);
        }

        yield return null; // Wait for one frame

        // Assert the maximum number of enemies were spawned
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Assert.AreEqual(maxEnemies, enemyCount -1, "Expected max enemies to be spawned.");

        // Clean up
        CleanupEnemies();
        Object.DestroyImmediate(enemyPrefab);
    }

    private GameObject CreateEnemyPrefab()
    {
        GameObject enemyPrefab = new GameObject("Enemy");
        enemyPrefab.AddComponent<Rigidbody2D>(); // Example component
        enemyPrefab.tag = "Enemy";
        Debug.Log("Enemy prefab created.");
        return enemyPrefab;
    }

    private void CleanupEnemies()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Object.DestroyImmediate(enemy);
        }
    }

    private Slime Spawn(GameObject slimePrefab, Vector3 position, Quaternion rotation)
    {
        // Instantiate the Slime prefab
        GameObject slimeInstance = Object.Instantiate(slimePrefab, position, rotation);
        return slimeInstance.GetComponent<Slime>(); // Return the Slime component from the instantiated object
    }
}
