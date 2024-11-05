using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChestInteractionStressTest
{
    private const float spawnRadius = 0.2f;
    private bool sceneLoaded = false;
    private int initialMoney;
    private int chestCount = 50;
    private GameObject chestPrefab;
    public PlayerHUD playerHUD;  // Assign your PlayerHUD instance in the Inspector
    public InventoryManager playerInventory; // Assign your InventoryManager instance in the Inspector

    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'PlayerRoom'...");
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("PlayerRoom", LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene 'PlayerRoom' loaded.");
        sceneLoaded = true;
    }

    [UnityTest]
    public IEnumerator TestMultipleChestInteractions()
    {
        // Wait for the scene to load before starting the test
        while (!sceneLoaded)
        {
            yield return null; // Wait until the scene is fully loaded
        }

        // Setup initial money
        initialMoney = playerHUD.GetMoney(); // Assuming a GetMoney method exists in PlayerHUD

        // Create the chest prefab
        chestPrefab = CreateChestPrefab();

        // Spawn multiple chests and interact with each
        for (int i = 0; i < chestCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition(spawnRadius);
            ChestInteraction spawnedChest = SpawnChest(chestPrefab, spawnPosition, Quaternion.identity);
            spawnedChest.playerHUD = playerHUD;
            spawnedChest.playerInventory = playerInventory;

            // Simulate player interaction with chest
            spawnedChest.isPlayerNearby = true; // Simulate player in range
            spawnedChest.CollectMoneyFromChest(); // Open the chest
            yield return null; // Wait a frame to mimic real gameplay delay
        }

        // Calculate expected final balance
        int expectedMoney = initialMoney + (chestCount);
        int finalMoney = playerHUD.GetMoney();

        // Verify if the final balance is correct
        Assert.AreEqual(expectedMoney, finalMoney, "Player's money amount after opening all chests is incorrect.");

        Debug.Log($"Stress Test Complete: Expected Money = {expectedMoney}, Final Money = {finalMoney}");

        // Clean up
        CleanupChests();
        Object.DestroyImmediate(chestPrefab);
    }

    private GameObject CreateChestPrefab()
    {
        // Create a chest prefab with necessary components
        GameObject chestPrefab = new GameObject("Chest");
        chestPrefab.AddComponent<ChestInteraction>(); // Add ChestInteraction script
        chestPrefab.AddComponent<Rigidbody2D>(); // Example component (if needed)
        chestPrefab.tag = "Chest";
        Debug.Log("Chest prefab created.");
        return chestPrefab;
    }

    private Vector3 GetRandomSpawnPosition(float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        return new Vector3(randomPoint.x, 0, randomPoint.y);
    }

    private ChestInteraction SpawnChest(GameObject chest, Vector3 position, Quaternion rotation)
    {
        // Instantiate the chest prefab
        GameObject chestInstance = Object.Instantiate(chest, position, rotation);
        return chestInstance.GetComponent<ChestInteraction>(); // Return the ChestInteraction component from the instantiated object
    }

    private void CleanupChests()
    {
        foreach (var chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            Object.DestroyImmediate(chest);
        }
    }
}
