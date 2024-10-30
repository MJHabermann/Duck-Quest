using System.Collections;
using System.Collections.Generic; 
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class EnemyStressTest
{
    public int maxEnemies = 200;  // Maximum number of enemies to spawn
    public float spawnDelay = 0.1f;  // Delay between each spawn
    private float lastSpawnTime; // Track the last time an enemy was spawned
    private int currentEnemies = 0;
    private bool sceneLoaded = false;
    private GameObject slimePrefab;  // Reference to the existing slime prefab in the scene
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // List to keep track of all spawned enemies

    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'PlayerRoom'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("Test Scene", LoadSceneMode.Single);
    }

    private void SceneManagerSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene 'Test Scene' loaded.");
        sceneLoaded = true;
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Wait for the scene to load before running the test
        while (!sceneLoaded)
        {
            yield return null;
        }

        // Find the existing SlimePrefab in the scene
        slimePrefab = FindSlimePrefabInScene();

        if (slimePrefab != null)
        {
            Debug.Log("Slime prefab found successfully.");
        }
        else
        {
            Debug.LogError("Slime prefab could not be found in the scene!");
            Assert.Fail("Failed to find the Slime prefab.");
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator SpawnEnemies()
    {
        Debug.Log($"Starting enemy spawn test, spawning up to {maxEnemies} enemies...");

        lastSpawnTime = Time.time; // Initialize the spawn timer

        while (currentEnemies < maxEnemies)
        {
            if (Time.time - lastSpawnTime >= spawnDelay)
            {
                SpawnEnemy(); // Spawn an enemy
                lastSpawnTime = Time.time; // Update the last spawn time
            }
            yield return null; // Wait until the next frame
        }

        // Once spawning is done, check the physics status
        Debug.Log("All enemies spawned. Checking room physics...");
        CheckAllEnemies(); // Check all spawned enemies after spawning
        yield return null;
    }

    private void SpawnEnemy()
    {

        if (slimePrefab != null)
        {
            // Generate a random position within a radius of 1.0f from (0, 0, 0)
            Vector2 randomPos = Random.insideUnitCircle * 1.0f;  // Radius of 1.0f
            Vector3 spawnPosition = new Vector3(randomPos.x, 0, randomPos.y);  // y = 0 for 2D ground level

            // Instantiate the enemy prefab at the random position
            GameObject spawnedEnemy = Object.Instantiate(slimePrefab, spawnPosition, Quaternion.identity);

            if (spawnedEnemy != null)
            {
                Debug.Log($"Enemy spawned at random position {spawnPosition}. Total enemies spawned: {currentEnemies + 1}");

                // Add the spawned enemy to the list
                spawnedEnemies.Add(spawnedEnemy);

                currentEnemies++;
            }
        }
        else
        {
            Debug.LogError("Failed to spawn enemy.");
        }
    }

    private void CheckEnemyPosition(GameObject enemy, float wallLeft, float wallRight, float wallBottom, float wallTop, int num)
    {
        Vector3 enemyPosition = enemy.transform.position;

        // Check if the enemy's position is beyond the wall boundaries
        if (enemyPosition.x < wallLeft || enemyPosition.x > wallRight ||
            enemyPosition.y < wallBottom || enemyPosition.y > wallTop)
        {
            Debug.Log($"Enemy {200-num} broke through the wall at position {enemyPosition}!");
            Assert.Pass($"Enemy {200-num} broke the wall at {enemyPosition}.");
        }

        // Check if the enemy is touching any wall colliders
        foreach (var wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
            if (wallCollider == null)
            {
                Debug.LogError($"Wall {wall.name} does not have a BoxCollider2D!");
                continue; // Skip this wall if it doesn't have a collider
            }

            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb != null && enemyRb.IsTouching(wallCollider))
            {
                Debug.LogWarning($"Enemy {enemy.name} is touching the wall {wall.name}!");
            }
        }
    }

    private void CheckAllEnemies()
    {
        float wallLeft = -5.0f;   
        float wallRight = 5.0f;   
        float wallBottom = -5.0f;  
        float wallTop = 5.0f;      

        int count = 1;
        foreach (var enemy in spawnedEnemies)
        {
            CheckEnemyPosition(enemy, wallLeft, wallRight, wallBottom, wallTop, count);
            count++;
        }
    }

    private GameObject FindSlimePrefabInScene()
    {
        // Find the SlimePrefab that already exists in the scene
        GameObject slimePrefab = GameObject.Find("SlimePrefab");

        if (slimePrefab == null)
        {
            Debug.LogError("SlimePrefab not found in the scene!");
        }

        return slimePrefab;
    }
}
