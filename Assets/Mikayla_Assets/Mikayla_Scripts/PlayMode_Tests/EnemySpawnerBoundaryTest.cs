using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemySpawnerBoundaryTest
{
    private const int maxEnemies = 400;  // Upper boundary for testing
    private const float spawnRadius = 50f; // Area to spawn enemies

    [UnityTest]
    public IEnumerator TestSpawnZeroEnemies()
{
    // Cleanup any existing enemies
    foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    {
        Object.DestroyImmediate(enemy);
    }

    // Create the enemy prefab
    GameObject enemyPrefab = new GameObject("Enemy");
    enemyPrefab.AddComponent<Rigidbody2D>(); // Example component
    enemyPrefab.tag = "Enemy";
    Debug.Log("Enemy prefab created for zero-enemy test.");
    // Disable the prefab to prevent it from being counted
    enemyPrefab.SetActive(false);

    // Create EnemySpawner with 0 enemies
    EnemySpawner spawner = new EnemySpawner(enemyPrefab, 0, spawnRadius);
    Debug.Log("EnemySpawner instance created with 0 enemies.");

    // Run the spawner
    spawner.SpawnEnemies();
    yield return null;  // Wait for one frame

    // Assert no enemies were spawned
    int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    Debug.Log($"Number of enemies spawned: {enemyCount -1}");
    Assert.AreEqual(0, enemyCount, "Expected no enemies to be spawned.");

    // Restore the original tag and cleanup
    Object.DestroyImmediate(enemyPrefab);
    yield return null;  // Wait for one frame

    // Verify cleanup
    enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    Debug.Log($"Number of enemies after cleanup: {enemyCount}");
    Assert.AreEqual(0, enemyCount, "Expected 0 enemies after cleanup.");
}

    [UnityTest]
    public IEnumerator TestSpawnMaxEnemies()
    {
        // Cleanup any existing enemies
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Object.DestroyImmediate(enemy);
        }
        // Create the enemy prefab
        GameObject enemyPrefab = new GameObject("Enemy");
        enemyPrefab.AddComponent<Rigidbody2D>(); // Example component
        enemyPrefab.tag = "Enemy";
        Debug.Log("Enemy prefab created for max-enemy test.");
        // Disable the prefab to prevent it from being counted
        enemyPrefab.SetActive(false);

        // Create EnemySpawner with maxEnemies
        EnemySpawner spawner = new EnemySpawner(enemyPrefab, maxEnemies, spawnRadius);
        Debug.Log($"EnemySpawner instance created with {maxEnemies} enemies.");

        // Run the spawner
        spawner.SpawnEnemies();
        yield return null;  // Wait for one frame

        // Assert that maxEnemies were spawned
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log($"Number of enemies spawned: {enemyCount}");
        Assert.AreEqual(maxEnemies, enemyCount, $"Expected {maxEnemies} enemies to be spawned.");

        // Cleanup
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Object.DestroyImmediate(enemy);
        }
        // Restore the original tag and cleanup
        Object.DestroyImmediate(enemyPrefab);
        yield return null;  // Wait for one frame
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log($"Number of enemies after cleanup: {enemyCount}");
        Assert.AreEqual(0, enemyCount, "Expected 0 enemies after cleanup.");
    }

    [UnityTest]
    public IEnumerator TestSpawnOneEnemy()
    {
        // Cleanup any existing enemies
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Object.DestroyImmediate(enemy);
        }
        // Test the edge case of spawning exactly one enemy
        GameObject enemyPrefab = new GameObject("Enemy");
        enemyPrefab.AddComponent<Rigidbody2D>();
        enemyPrefab.tag = "Enemy";
        Debug.Log("Enemy prefab created for one-enemy test.");
        // Disable the prefab to prevent it from being counted
        enemyPrefab.SetActive(false);

        // Create EnemySpawner with 1 enemy
        EnemySpawner spawner = new EnemySpawner(enemyPrefab, 1, spawnRadius);
        Debug.Log("EnemySpawner instance created with 1 enemy.");

        // Run the spawner
        spawner.SpawnEnemies();
        yield return null;

        // Assert exactly one enemy was spawned
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log($"Number of enemies spawned: {enemyCount}");
        Assert.AreEqual(1, enemyCount, "Expected 1 enemy to be spawned.");

        // Cleanup
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Object.DestroyImmediate(enemy);
        }
        // Restore the original tag and cleanup
        Object.DestroyImmediate(enemyPrefab);
        Object.DestroyImmediate(enemyPrefab);
        yield return null;  // Wait for one frame
        // Verify cleanup
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log($"Number of enemies after cleanup: {enemyCount}");
        Assert.AreEqual(0, enemyCount, "Expected 0 enemies after cleanup.");
    }
}
