using UnityEngine;

public class EnemySpawner
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float spawnRadius;

    public EnemySpawner(GameObject prefab, int count, float radius)
    {
        enemyPrefab = prefab;
        numberOfEnemies = count;
        spawnRadius = radius;
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 0; // Assuming a flat terrain
            GameObject enemyInstance = Object.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyInstance.SetActive(true); // Ensure the enemy is active
        }
    }
}


