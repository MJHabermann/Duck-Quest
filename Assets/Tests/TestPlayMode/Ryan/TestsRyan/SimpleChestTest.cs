using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SimpleChestTest
{
    public GameObject chestPrefab;
    private GameObject chest;
    private GameObject player;

    [SetUp]
    public void SetUp()
    {
        // Set up chest and player in the scene
        chest = Object.Instantiate(chestPrefab, Vector3.zero, Quaternion.identity);
        player = new GameObject("Player");
        player.tag = "Player"; // Ensure player has the "Player" tag for interaction

        // Assign necessary components
        var chestInteraction = chest.GetComponent<ChestInteraction>();
        chestInteraction.playerHUD = new GameObject("MockPlayerHUD").AddComponent<PlayerHUD>();
        chestInteraction.playerInventory = new GameObject("MockInventoryManager").AddComponent<InventoryManager>();
    }

    [UnityTest]
    public IEnumerator TestChestCanBeOpened()
    {
        // Move player to chest position
        player.transform.position = chest.transform.position;
        yield return null;

        // Simulate interaction
        var chestInteraction = chest.GetComponent<ChestInteraction>();
        chestInteraction.CollectMoneyFromChest();
        Assert.IsTrue(chestInteraction.chestHasBeenOpened, "Chest should be opened after interaction.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(chest);
        Object.Destroy(player);
    }
}
