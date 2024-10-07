using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class SlimePlayModeTests
{
    private GameObject player;
    private GameObject slimeObject;
    public Slime slime;
    bool sceneLoaded;

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
    public IEnumerator SetUp()
    {
        // Ensure the scene is fully loaded before setting up the objects
        while (!sceneLoaded)
        {
            yield return null;
        }

        // Create a player GameObject
        player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = Vector3.zero;

        // Create a Slime GameObject
        slimeObject = new GameObject("Slime");
        slime = slimeObject.AddComponent<Slime>();
        slime.target = player.transform;
        slime.chaseRadius = 5f;
        slime.attackRadius = 1.0f;
        slime.homePosition = slimeObject.transform;

        Debug.Log("SetUp complete: Player position is " + player.transform.position + 
                   " and Slime position is " + slime.transform.position);

        // Ensure the objects are not null
        Assert.NotNull(player, "Player object should not be null after setup.");
        Assert.NotNull(slimeObject, "Slime object should not be null after setup.");
        Assert.NotNull(slime, "Slime component should not be null after setup.");

        // Wait for a frame to ensure everything is set up correctly
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Debug.Log("TearDown: Destroying player and slime objects.");

        // Ensure that objects exist before destroying them
        if (player != null)
        {
            Object.DestroyImmediate(player);
            Debug.Log("Player object destroyed.");
        }
        else
        {
            Debug.LogWarning("Player object was already null.");
        }

        if (slimeObject != null)
        {
            Object.DestroyImmediate(slimeObject);
            Debug.Log("Slime object destroyed.");
        }
        else
        {
            Debug.LogWarning("Slime object was already null.");
        }
        
        // Wait a frame to ensure destruction completes
        yield return null;
    }

    [UnityTest]
    public IEnumerator SlimeDoesNotMoveOutsideChaseRadius()
    {
        // Ensure the objects are not null
        Assert.NotNull(player, "Player object should not be null.");
        Assert.NotNull(slimeObject, "Slime object should not be null.");
        Assert.NotNull(slime, "Slime component should not be null.");

        // Move the player outside the chase radius
        player.transform.position = new Vector3(6.0f, 0, 0);
        Debug.Log("Moving player to: " + player.transform.position);

        // Wait for a frame to let the Slime update
        yield return new WaitForSeconds(0.1f); // Simulate time for Slime behavior

        // Check that the Slime has not moved
        Debug.Log("Checking Slime position after attempting to chase: " + slime.transform.position);
        Assert.AreEqual(Vector3.zero, slime.transform.position);
    }

    [UnityTest]
    public IEnumerator SlimeStaysOutsideAttackRadius()
    {
        // Ensure the objects are not null
        Assert.NotNull(player, "Player object should not be null.");
        Assert.NotNull(slimeObject, "Slime object should not be null.");
        Assert.NotNull(slime, "Slime component should not be null.");

        // Move the player within the attack radius
        player.transform.position = new Vector3(0.5f, 0, 0);
        Debug.Log("Moving player to: " + player.transform.position);

        // Wait for a frame to let the Slime update
        yield return new WaitForSeconds(0.1f); // Simulate time for Slime behavior

        // Check that the Slime has not moved
        Debug.Log("Checking Slime position after being too close: " + slime.transform.position);
        Assert.AreEqual(Vector3.zero, slime.transform.position);
    }
}

