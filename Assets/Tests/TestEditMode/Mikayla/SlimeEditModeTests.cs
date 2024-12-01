using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
/*
public class SlimeEditModeTests
{
    private GameObject player;
    private GameObject slimeObject;
    //public Slime slime;

    [SetUp]
    public void SetUp()
    {
        // Create a player GameObject
        player = new GameObject();
        player.tag = "Player";
        player.transform.position = Vector3.zero;

        // Create a Slime GameObject
        slimeObject = new GameObject();
        slime = slimeObject.AddComponent<Slime>();
        slime.target = player.transform;
        slime.chaseRadius = 2f;
        slime.attackRadius = 1.0f;
        slime.homePosition = slimeObject.transform;

        Debug.Log("SetUp complete: Player position is " + player.transform.position + 
                   " and Slime position is " + slime.transform.position);
    }

    [TearDown]
    public void TearDown()
    {
        Debug.Log("TearDown: Destroying player and slime objects.");
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(slimeObject);
    }

    // [UnityTest]
    // public IEnumerator SlimeStaysWithinChaseRadius()
    // {
    //     // Move the player within the chase radius but outside the attack radius
    //     player.transform.position = new Vector3(3.0f, 0, 0);
    //     Debug.Log("Moving player to: " + player.transform.position);

    //     // Wait for a frame to let the Slime update
    //     yield return null;

    //     // Check that the Slime has moved towards the player
    //     Debug.Log("Checking Slime position after move: " + slime.transform.position);
    //     Assert.AreEqual(new Vector3(3.0f, 0, 0), slime.transform.position);
    // }

    [UnityTest]
    public IEnumerator SlimeDoesNotMoveOutsideChaseRadius()
    {
        // Move the player outside the chase radius
        player.transform.position = new Vector3(6.0f, 0, 0);
        Debug.Log("Moving player to: " + player.transform.position);

        // Wait for a frame to let the Slime update
        yield return null;

        // Check that the Slime has not moved
        Debug.Log("Checking Slime position after attempting to chase: " + slime.transform.position);
        Assert.AreEqual(Vector3.zero, slime.transform.position);
    }

    [UnityTest]
    public IEnumerator SlimeStaysOutsideAttackRadius()
    {
        // Move the player within the attack radius
        player.transform.position = new Vector3(0.5f, 0, 0);
        Debug.Log("Moving player to: " + player.transform.position);

        // Wait for a frame to let the Slime update
        yield return null;

        // Check that the Slime has not moved
        Debug.Log("Checking Slime position after being too close: " + slime.transform.position);
        Assert.AreEqual(Vector3.zero, slime.transform.position);
    }
}
*/