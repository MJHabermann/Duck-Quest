using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Profiling;

public class EnemySpawnerTest
{
    [UnityTest]
    public IEnumerator StressTestEnemySpawner()
    {
        // Set up the test environment
        GameObject enemyPrefab = new GameObject("Enemy");
        enemyPrefab.AddComponent<Rigidbody2D>(); // Example component
        enemyPrefab.tag = "Enemy";
        Debug.Log("Enemy prefab created and Rigidbody added.");

        // Disable the prefab to prevent it from being counted
        enemyPrefab.SetActive(false);

        int initialEnemyCount = 10; // Start with 10 enemies
        int maxEnemies = 1000000; // Safety limit to prevent crashing the editor
        float spawnRadius = 50f; // Spawn radius
        int currentEnemyCount = initialEnemyCount;
        bool systemFailed = false;

        // A list to store all spawned enemies for cleanup later
        List<GameObject> spawnedEnemies = new List<GameObject>();

        while (currentEnemyCount <= maxEnemies && !systemFailed)
        {
            Debug.Log($"Spawning {currentEnemyCount} enemies.");

            // Create the spawner and spawn the enemies
            EnemySpawner spawner = new EnemySpawner(enemyPrefab, currentEnemyCount, spawnRadius);
            spawner.SpawnEnemies();

            // Wait a frame to allow spawning to complete
            yield return null;

            // Check how many enemies are spawned
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; // Count the active enemies
            Debug.Log($"Number of enemies spawned: {enemyCount}");

            // If the system can't handle the load or the number of enemies is incorrect, break the loop
            if (enemyCount != currentEnemyCount || SystemPerformanceIsDegrading())
            {
                systemFailed = true;
                Debug.LogWarning($"System failed with {currentEnemyCount} enemies.");
            }
            else
            {
                // Check if doubling the current count would exceed maxEnemies
                if (currentEnemyCount <= maxEnemies / 2)
                {
                    // Double the number of enemies for the next iteration
                    currentEnemyCount *= 2;
                }
                else
                {
                    // If doubling would exceed the limit, set it to maxEnemies
                    currentEnemyCount = maxEnemies;
                }
            }

            // Store the spawned enemies for later cleanup
            spawnedEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

            // Cleanup for the next round
            foreach (var enemy in spawnedEnemies)
            {
                Object.Destroy(enemy);
            }
            spawnedEnemies.Clear();
        }

        if (systemFailed)
        {
            Debug.Log($"System broke when trying to spawn {currentEnemyCount} enemies.");
        }
        else
        {
            Debug.Log($"Test completed without system failure. Max enemies tested: {currentEnemyCount}.");
        }

        // Clean up the prefab
        Object.Destroy(enemyPrefab);
    }

    private bool SystemPerformanceIsDegrading()
    {
        // Set a memory threshold (in bytes). You can adjust this value based on your needs.
        const long memoryThreshold = 200 * 1024 * 1024; // 200 MB as an example threshold

        // Get current memory usage
        long totalAllocatedMemory = Profiler.GetTotalAllocatedMemoryLong();

        // Check if the allocated memory exceeds the threshold
        if (totalAllocatedMemory > memoryThreshold)
        {
            Debug.LogWarning($"Memory overload detected: {totalAllocatedMemory / (1024 * 1024)} MB");
            return true; // Indicates memory overload
        }

        return false; // No overload
    }
}

