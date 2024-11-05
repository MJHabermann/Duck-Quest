using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class InventoryStress
{
    private bool sceneLoaded = false;
    private const float loadTimeout = 5f; // Timeout for scene loading

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

    [UnitySetUp]
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
    }

[UnityTest]
public IEnumerator AddInventoryItems()
{
    Debug.Log("Starting AddInventoryItems stress test...");

    // Wait a frame to make sure the setup is complete
    yield return null;

    // Locate the inventory GameObject in the scene
    GameObject inventoryObject = GameObject.Find("Inventory 1");
    Assert.IsNotNull(inventoryObject, "Inventory GameObject not found in the scene.");

    // Get the InventoryManager component
    var inventoryComponent = inventoryObject.GetComponent<InventoryManager>();
    Assert.IsNotNull(inventoryComponent, "InventoryManager component not found on the Inventory GameObject.");

    // Record the initial item count
    int initialItemCount = inventoryComponent.GetItemCount();

    // Define parameters for the items to add
    string itemName = "TestItem";
    int quantity = 1;
    Sprite itemSprite = null; // Use null for this example, or assign a test sprite if available
    string itemDescription = "A test item used for stress testing.";

    int itemsToAdd = 99; // Define how many items you want to add

    // Loop to add multiple items to the inventory
    for (int i = 0; i < itemsToAdd; i++)
    {
        inventoryComponent.AddItem(itemName + i, quantity, itemSprite, itemDescription);
    }

    // Wait a frame to let any internal processes update, if necessary
    yield return null;

    // Calculate the expected final item count with max capacity in mind
    int expectedFinalCount = 10;

    // Verify the expected final item count
    int finalItemCount = inventoryComponent.GetItemCount();
    Assert.AreEqual(expectedFinalCount, finalItemCount, $"Expected item count to be {expectedFinalCount}, but found {finalItemCount}.");

    Debug.Log($"Inventory stress test completed successfully. Added {itemsToAdd} items.");

    yield return null;
}

}