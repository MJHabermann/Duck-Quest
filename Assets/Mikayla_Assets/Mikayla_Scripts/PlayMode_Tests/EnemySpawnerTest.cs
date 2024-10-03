using System.Collections;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class EnemyStressTest
{
    public int maxEnemies = 1000; // Maximum number of enemies to spawn
    public float spawnDelay = 0.1f; // Delay between each spawn
    private int currentEnemies = 0;
    private bool sceneLoaded = false;
    private GameObject slimePrefab;

    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'PlayerRoom'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("PlayerRoom", LoadSceneMode.Single);
    }

    private void SceneManagerSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene 'PlayerRoom' loaded.");
        sceneLoaded = true;
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Wait for the scene to load before running the test
        while (!sceneLoaded)
        {
            yield return null; // Wait until the scene is fully loaded
        }

        // Assign the slime prefab
        slimePrefab = CreateEnemyPrefab();

        if (slimePrefab != null)
        {
            Debug.Log("Slime prefab created successfully.");
        }
        else
        {
            Debug.LogError("Slime prefab could not be created!");
            Assert.Fail("Failed to create or assign the Slime prefab.");
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator SpawnEnemies()
    {
        Debug.Log($"Starting enemy spawn test, spawning up to {maxEnemies} enemies...");

        if (slimePrefab == null)
        {
            Debug.LogError("Slime prefab is null! Cannot spawn enemies.");
            Assert.Fail("Slime prefab is null. Check prefab setup.");
            yield break;
        }

        while (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay); // Delay between spawns
        }

        // Once spawning is done, check the physics status
        Debug.Log("All enemies spawned. Checking room physics...");
        CheckRoomPhysics();

        yield return null;
    }

    public void SpawnEnemy()
    {
        if (slimePrefab == null)
        {
            Debug.LogError("Slime prefab is null! Cannot spawn enemy.");
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedEnemy = Object.Instantiate(slimePrefab, spawnPosition, Quaternion.identity);

        if (spawnedEnemy != null)
        {
            Debug.Log($"Enemy spawned at position {spawnPosition}. Total enemies spawned: {currentEnemies + 1}");
            currentEnemies++;
        }
        else
        {
            Debug.LogError("Failed to spawn enemy.");
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomPos = Random.insideUnitCircle * 0.2f;
        return new Vector3(randomPos.x, 0, randomPos.y); // Assuming y=0 for ground level
    }

void CheckRoomPhysics()
{
    Rigidbody2D[] rigidbodies = Object.FindObjectsOfType<Rigidbody2D>();

    if (rigidbodies.Length == 0)
    {
        Debug.LogWarning("No Rigidbody2D objects found in the scene.");
        return;
    }
    int count = 0;
    foreach (Rigidbody2D rb in rigidbodies)
    {
        if (rb == null)
        {
            Debug.LogError("Found a null Rigidbody2D!");
            continue;
        }

        if (rb.IsSleeping())
        {
            Debug.LogWarning($"Rigidbody2D {rb.name} is sleeping! Unexpected behavior at {count}");
        }
        else
        {
            Debug.Log($"Rigidbody2D {rb.name} is behaving as expected.");
        }
        count +=1;
    }
}

    private GameObject CreateEnemyPrefab()
{
    // Create the enemy prefab GameObject
    GameObject enemyPrefab = new GameObject("SlimePrefab");

    // Add components to the prefab
    enemyPrefab.AddComponent<Rigidbody2D>();               // For physics
    enemyPrefab.AddComponent<BoxCollider2D>();             // Collider for interactions
    enemyPrefab.tag = "Enemy"; // Tagging the object as "Enemy"
    
    Debug.Log("Enemy prefab created.");
    return enemyPrefab;
}

}

