using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
/*
public class SlimeTests
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
                // Wait for a frame to ensure everything is set up correctly
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerExists()
    {
        // wait for scene to call start()
        yield return new WaitForSeconds(1f);
        GameObject player = GameObject.Find("Player");

        // Assert player object exists
        Assert.IsTrue(player);
    }

    [Test]
    public void EnemyTypeIsSlime(){
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy slime = enemy.GetComponent<Enemy>();

        Assert.IsTrue(slime.EnemyName=="Slime");
    }
    
    [Test]
    public void SlimeExists()
    {
        // Find the player object in the scene
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Assert that the enemy object exists
        Assert.IsNotNull(enemy);
    }

    [Test]
    public void IsHealthSet()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy slime = enemy.GetComponent<Enemy>();

        Assert.IsTrue(slime.Health==3);
    }
        [Test]
    public void DoesEnemyHaveHealth()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy slime = enemy.GetComponent<Enemy>();

        Assert.IsTrue(slime.Health>0);
    }

    [UnityTest]
    public IEnumerator SlimeIsIdle()
    {
        yield return new WaitForSeconds(1f);
        GameObject enemyObject = GameObject.FindWithTag("Enemy");
        Rigidbody2D enemy = enemyObject.GetComponent<Rigidbody2D>();
        Assert.IsTrue(enemy.velocity.x == 0);
    }

    [UnityTest]
    public IEnumerator SlimeMovesInsideChaseRadius()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject slime = GameObject.FindWithTag("Enemy");
        GameObject player = GameObject.FindWithTag("Player");
        // Ensure the objects are not null
        Assert.NotNull(player, "Player object should not be null.");
        Assert.NotNull(slime, "Slime component should not be null.");

        Vector3 initial_pos = slime.transform.position;
        // Move the player outside the chase radius
        player.transform.position = new Vector3(-1.0f, -1, 0);
        Debug.Log("Moving player to: " + player.transform.position);

        // Wait for a frame to let the Slime update
        yield return new WaitForSeconds(0.5f); // Simulate time for Slime behavior

        // Check that the Slime has not moved
        Debug.Log("Checking Slime position after attempting to chase: " + slime.transform.position);
        Assert.AreNotEqual(initial_pos, slime.transform.position);
    }

    [UnityTest]
    public IEnumerator SlimeDoesNotMoveOutsideChaseRadius()
    {
        GameObject slime = GameObject.FindWithTag("Enemy");
        GameObject player = GameObject.FindWithTag("Player");
        // Ensure the objects are not null
        Assert.NotNull(player, "Player object should not be null.");
        Assert.NotNull(slime, "Slime component should not be null.");
        Vector3 initial_pos = slime.transform.position;
        // Move the player within the attack radius
        player.transform.position = new Vector3(0.5f, 0, 0);
        Debug.Log("Moving player to: " + player.transform.position);

        // Wait for a frame to let the Slime update
        yield return new WaitForSeconds(0.1f); // Simulate time for Slime behavior

        // Check that the Slime has not moved
        Debug.Log("Checking Slime position after being too close: " + slime.transform.position);
        Assert.AreEqual(initial_pos, slime.transform.position);
    }
}
*/
