using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;
public class InventoryTest
{
     private bool sceneLoaded = false;
    private const float loadTimeout = 5f; // Timeout for scene loading

    private InventoryManager inventoryComponent;

    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Starting to load scene 'Town'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("Town", LoadSceneMode.Single);
    }

    private void SceneManagerSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Town")
        {
            Debug.Log("Scene 'Town' loaded successfully.");
            sceneLoaded = true;
        }
        else
        {
            Debug.LogWarning($"Unexpected scene loaded: {scene.name}");
        }
    }

    [OneTimeSetUp]
    public IEnumerator SetUp()
    {
        float timer = 0f;

        // Wait for the scene to load with a timeout
        while (!sceneLoaded && timer < loadTimeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (!sceneLoaded)
        {
            Debug.LogError("Scene 'Town' failed to load within the timeout period.");
            Assert.Fail("Scene 'Town' did not load within the expected time.");
        }

        // Confirm the scene is active
        Assert.AreEqual("Town", SceneManager.GetActiveScene().name, "Failed to load the 'Town' scene as the active scene.");
        Debug.Log("Scene setup complete and confirmed.");

        // Wait one additional frame to ensure everything is set up correctly
        yield return null;
            Debug.Log("Starting AddInventoryItems stress test...");

        // Wait a frame to make sure the setup is complete
        yield return null;

        // Locate the inventory GameObject in the scene
        GameObject inventoryObject = GameObject.Find("Inventory 1");
        Assert.IsNotNull(inventoryObject, "Inventory GameObject not found in the scene.");

        // Get the InventoryManager component
        var inventoryComponent = inventoryObject.GetComponent<InventoryManager>();
        Assert.IsNotNull(inventoryComponent, "InventoryManager component not found on the Inventory GameObject.");
    }

    // 1. Add Item to Empty Inventory
    [UnityTest]
    public IEnumerator AddItemToEmptyInventory()
    {
        // Arrange
        string itemName = "TestItem";
        int quantity = 1;
        Sprite itemSprite = null;
        string itemDescription = "A test item.";

        // Act
        inventoryComponent.AddItem(itemName, quantity, itemSprite, itemDescription);

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(1, totalItemCount, "Inventory should have 1 item after adding to empty inventory.");

        var firstSlot = inventoryComponent.itemSlot[0];
        Assert.AreEqual(itemName, firstSlot.itemName, "First slot should contain the added item.");
        Assert.AreEqual(quantity, firstSlot.quantity, "First slot should have a quantity of 1.");
    }

    // 2. Add Multiple Items to a Single Slot
    [UnityTest]
    public IEnumerator AddMultipleItemsToSingleSlot()
    {
        // Arrange
        string itemName = "TestItem";
        int initialQuantity = 5;
        int additionalQuantity = 3;
        Sprite itemSprite = null;
        string itemDescription = "A test item.";

        // Act
        inventoryComponent.AddItem(itemName, initialQuantity, itemSprite, itemDescription);
        inventoryComponent.AddItem(itemName, additionalQuantity, itemSprite, itemDescription);

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(initialQuantity + additionalQuantity, totalItemCount, "Inventory should have correct total item count.");

        var firstSlot = inventoryComponent.itemSlot[0];
        Assert.AreEqual(itemName, firstSlot.itemName, "First slot should contain the added item.");
        Assert.AreEqual(initialQuantity + additionalQuantity, firstSlot.quantity, "First slot should have the correct total quantity.");
    }

    // 3. Slot Maximum Capacity Enforcement
    [UnityTest]
    public IEnumerator SlotMaximumCapacityEnforcement()
    {
        // Arrange
        string itemName = "TestItem";
        int quantityToAdd = 15; // Exceeds single slot capacity
        Sprite itemSprite = null;
        string itemDescription = "A test item.";
        int slotMaxCapacity = 9; // Assuming max capacity per slot is 9

        // Act
        inventoryComponent.AddItem(itemName, quantityToAdd, itemSprite, itemDescription);

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(quantityToAdd, totalItemCount, "Inventory should have correct total item count.");

        var firstSlot = inventoryComponent.itemSlot[0];
        var secondSlot = inventoryComponent.itemSlot[1];

        Assert.AreEqual(slotMaxCapacity, firstSlot.quantity, "First slot should be full with max capacity.");
        Assert.AreEqual(quantityToAdd - slotMaxCapacity, secondSlot.quantity, "Second slot should have the remaining items.");
    }

    // 4. Inventory Maximum Capacity Enforcement
    [UnityTest]
    public IEnumerator InventoryMaximumCapacityEnforcement()
    {
        // Arrange
        string itemName = "TestItem";
        int quantityToAdd = 100; // Exceeds total inventory capacity
        Sprite itemSprite = null;
        string itemDescription = "A test item.";
        int slotMaxCapacity = 9; // Max items per slot
        int totalSlots = inventoryComponent.itemSlot.Length;
        int inventoryMaxCapacity = slotMaxCapacity * totalSlots;

        // Act
        int leftoverItems = inventoryComponent.AddItem(itemName, quantityToAdd, itemSprite, itemDescription);

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(inventoryMaxCapacity, totalItemCount, "Inventory should have max capacity of items.");
        Assert.AreEqual(quantityToAdd - inventoryMaxCapacity, leftoverItems, "Leftover items should be equal to items that couldn't be added.");

        // Ensure all slots are full
        foreach (var slot in inventoryComponent.itemSlot)
        {
            Assert.AreEqual(slotMaxCapacity, slot.quantity, "Each slot should be full with max capacity.");
        }
    }

    // 5. Add Different Items to Inventory
    [UnityTest]
    public IEnumerator AddDifferentItemsToInventory()
    {
        // Arrange
        string itemNameA = "TestItemA";
        int quantityA = 5;
        string itemNameB = "TestItemB";
        int quantityB = 3;
        Sprite itemSprite = null;
        string itemDescription = "A test item.";

        // Act
        inventoryComponent.AddItem(itemNameA, quantityA, itemSprite, itemDescription);
        inventoryComponent.AddItem(itemNameB, quantityB, itemSprite, itemDescription);

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(quantityA + quantityB, totalItemCount, "Inventory should have correct total item count.");

        var firstSlot = inventoryComponent.itemSlot[0];
        var secondSlot = inventoryComponent.itemSlot[1];

        Assert.AreEqual(itemNameA, firstSlot.itemName, "First slot should contain TestItemA.");
        Assert.AreEqual(quantityA, firstSlot.quantity, "First slot should have correct quantity.");

        Assert.AreEqual(itemNameB, secondSlot.itemName, "Second slot should contain TestItemB.");
        Assert.AreEqual(quantityB, secondSlot.quantity, "Second slot should have correct quantity.");
    }
/*
    // 6. Remove Item from Inventory
    [UnityTest]
    public IEnumerator RemoveItemFromInventory()
    {
        // Arrange
        string itemName = "TestItem";
        int initialQuantity = 5;
        int quantityToRemove = 2;
        Sprite itemSprite = null;
        string itemDescription = "A test item.";

        // Act
        inventoryComponent.AddItem(itemName, initialQuantity, itemSprite, itemDescription);

        // Simulate item usage/removal
        inventoryComponent.Item(itemName, quantityToRemove);

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(initialQuantity - quantityToRemove, totalItemCount, "Inventory should have correct total item count after removal.");

        var firstSlot = inventoryComponent.itemSlot[0];
        Assert.AreEqual(initialQuantity - quantityToRemove, firstSlot.quantity, "First slot should have correct quantity after removal.");
    }

    // 7. Use Item Until Depletion
    [UnityTest]
    public IEnumerator UseItemUntilDepletion()
    {
        // Arrange
        string itemName = "ConsumableItem";
        int initialQuantity = 3;
        Sprite itemSprite = null;
        string itemDescription = "A consumable test item.";

        // Act
        inventoryComponent.AddItem(itemName, initialQuantity, itemSprite, itemDescription);

        // Use the item three times
        for (int i = 0; i < initialQuantity; i++)
        {
            inventoryComponent.UseItem(itemName);
        }

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(0, totalItemCount, "Inventory should be empty after using all items.");

        var firstSlot = inventoryComponent.itemSlot[0];
        Assert.AreEqual(0, firstSlot.quantity, "First slot should be empty.");
        Assert.IsFalse(firstSlot.isFull, "First slot should not be full.");
    }
    */

    // 8. Add Item to Full Inventory
    [UnityTest]
    public IEnumerator AddItemToFullInventory()
    {
        // Arrange
        string itemName = "TestItem";
        Sprite itemSprite = null;
        string itemDescription = "A test item.";
        int slotMaxCapacity = 9;
        int totalSlots = inventoryComponent.itemSlot.Length;
        int inventoryMaxCapacity = slotMaxCapacity * totalSlots;

        // Fill the inventory to max capacity
        inventoryComponent.AddItem(itemName, inventoryMaxCapacity, itemSprite, itemDescription);

        // Attempt to add one more item
        int leftoverItems = inventoryComponent.AddItem("ExtraItem", 1, itemSprite, "An extra item.");

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(inventoryMaxCapacity, totalItemCount, "Inventory should still be at max capacity.");
        Assert.AreEqual(1, leftoverItems, "Leftover items should be 1 as inventory is full.");

        // Ensure all slots are full
        foreach (var slot in inventoryComponent.itemSlot)
        {
            Assert.AreEqual(slotMaxCapacity, slot.quantity, "Each slot should be full.");
        }
    }

    // 9. Check Inventory After Scene Load
    [UnityTest]
    public IEnumerator CheckInventoryAfterSceneLoad()
    {
        // Arrange
        string itemName = "PersistentItem";
        int quantity = 5;
        Sprite itemSprite = null;
        string itemDescription = "An item that persists across scenes.";

        // Act
        inventoryComponent.AddItem(itemName, quantity, itemSprite, itemDescription);

        // Save inventory state if necessary
        // Assuming inventory data is persistent

        // Load another scene and then reload 'Town'
        SceneManager.LoadScene("PlayerRoom", LoadSceneMode.Single);
        yield return new WaitForSeconds(1f); // Wait for the scene to load
        SceneManager.LoadScene("Town", LoadSceneMode.Single);

        // Wait for the scene to reload
        yield return new WaitForSeconds(1f);

        // Re-fetch the inventory component after scene load
        GameObject inventoryObject = GameObject.Find("Inventory 1");
        Assert.IsNotNull(inventoryObject, "Inventory GameObject not found after scene reload.");
        inventoryComponent = inventoryObject.GetComponent<InventoryManager>();
        Assert.IsNotNull(inventoryComponent, "InventoryManager component not found after scene reload.");

        // Wait a frame for any updates
        yield return null;

        // Assert
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(quantity, totalItemCount, "Inventory should retain items after scene load.");

        var firstSlot = inventoryComponent.itemSlot[0];
        Assert.AreEqual(itemName, firstSlot.itemName, "First slot should contain the persistent item.");
        Assert.AreEqual(quantity, firstSlot.quantity, "First slot should have correct quantity.");
    }

    // 10. Concurrent Item Addition
    [UnityTest]
    public IEnumerator ConcurrentItemAddition()
    {
        // Arrange
        string itemName = "TestItem";
        int quantityPerAdd = 1;
        int numberOfAdds = 50;
        Sprite itemSprite = null;
        string itemDescription = "A test item.";

        // Act
        for (int i = 0; i < numberOfAdds; i++)
        {
            inventoryComponent.AddItem(itemName, quantityPerAdd, itemSprite, itemDescription);
        }

        // Wait a frame for any updates
        yield return null;

        // Assert
        int expectedTotal = quantityPerAdd * numberOfAdds;
        int totalItemCount = inventoryComponent.GetItemCount();
        Assert.AreEqual(expectedTotal, totalItemCount, "Inventory should have correct total item count after concurrent additions.");

        // Verify items are distributed correctly among slots
        int slotMaxCapacity = 9; // Assuming max capacity per slot is 9
        int fullSlots = expectedTotal / slotMaxCapacity;
        int remainingItems = expectedTotal % slotMaxCapacity;

        for (int i = 0; i < inventoryComponent.itemSlot.Length; i++)
        {
            var slot = inventoryComponent.itemSlot[i];
            if (i < fullSlots)
            {
                Assert.AreEqual(slotMaxCapacity, slot.quantity, $"Slot {i} should be full.");
            }
            else if (i == fullSlots && remainingItems > 0)
            {
                Assert.AreEqual(remainingItems, slot.quantity, $"Slot {i} should have remaining items.");
            }
            else
            {
                Assert.AreEqual(0, slot.quantity, $"Slot {i} should be empty.");
            }
        }
    }

    // Helper method to clear the inventory (to be implemented in InventoryManager)
    // You need to add this method to your InventoryManager script
    /*
    public void ClearInventory()
    {
        foreach (var slot in itemSlot)
        {
            slot.ClearSlot();
        }
        totalItemCount = 0;
    }
    */

    // Similarly, you'll need to implement RemoveItem and possibly other methods in your InventoryManager and ItemSlot classes.
}




