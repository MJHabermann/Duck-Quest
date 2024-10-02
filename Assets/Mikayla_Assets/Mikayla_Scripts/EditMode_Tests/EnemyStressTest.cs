using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyStressTest
{
    [field: SerializeField] GameObject slimePrefab; // Assign your Slime prefab in the Inspector
    private int initialNumberOfSlimes = 1; // Starting number of Slimes
    private float spawnRadius = 50f; // Radius within which to spawn Slimes
    private int maxEnemiesAllowed = 100000; // Safety limit to prevent endless loops

    [SetUp]
    public void SetUp()
    {
        // Load the prefab from Resources or assign it directly if available
        slimePrefab = GameObject.FindWithTag("Enemy");

        // Assert that prefab is properly assigned to avoid null reference errors
        Assert.NotNull(slimePrefab, "Slime prefab not found. Ensure the prefab is tagged 'Enemy' and available in the scene.");
    }

    [Test]
    public void EnemyStressTestSimplePasses()
    {
        var enumerator = EnemyStressTestWithEnumeratorPasses();
        while (enumerator.MoveNext()) { }
    }

    [UnityTest]
    public IEnumerator EnemyStressTestWithEnumeratorPasses()
    {
        int currentNumberOfSlimes = initialNumberOfSlimes;
        bool systemFailed = false;
        List<GameObject> spawnedEnemies = new List<GameObject>();

        while (!systemFailed && currentNumberOfSlimes <= maxEnemiesAllowed)
        {
            Debug.Log($"Spawning {currentNumberOfSlimes} enemies.");

            // Try spawning enemies
            for (int i = 0; i < currentNumberOfSlimes; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    0,
                    Random.Range(-spawnRadius, spawnRadius)
                );

                GameObject enemy = Object.Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
                spawnedEnemies.Add(enemy);

                // Check if performance issues arise (e.g., low FPS or system overload)
                if (SystemPerformanceIsDegrading())
                {
                    systemFailed = true;
                    Debug.LogWarning($"System failed with {currentNumberOfSlimes} enemies spawned.");
                    break;
                }

                yield return null; // Wait a frame to avoid freezing the game
            }

            // Clean up previous enemies before next round
            foreach (var enemy in spawnedEnemies)
            {
                Object.DestroyImmediate(enemy);
            }
            spawnedEnemies.Clear();

            if (!systemFailed)
            {
                // Double the number of slimes for the next iteration
                currentNumberOfSlimes *= 2;
            }
        }

        // Output the breaking point
        if (systemFailed)
        {
            Debug.Log($"System failed when trying to spawn {currentNumberOfSlimes} enemies.");
        }
        else
        {
            Debug.Log($"Test completed without system failure, max enemies tested: {currentNumberOfSlimes}.");
        }
    }

    // Placeholder for checking system performance. You can replace this with actual performance checks.
    private bool SystemPerformanceIsDegrading()
    {
        // You can replace this with actual performance checks, like checking FPS, memory usage, etc.
        // For now, we simulate a system breaking when too many enemies are spawned.
        return Random.Range(0f, 1f) < 0.01f; // Randomly fail for testing purposes (1% chance)
    }
}
