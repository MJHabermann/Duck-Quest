using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoblinTests
{
    bool sceneLoaded;
    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'Town'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("Town", LoadSceneMode.Single);
    }

    private void SceneManagerSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene 'Town' loaded.");
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
    public void EnemyTypeIsGoblin(){
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy goblin = enemy.GetComponent<Enemy>();

        Assert.IsTrue(goblin.EnemyName=="Goblin");
    }
    
    [Test]
    public void doesGoblinExist()
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
        Enemy goblin = enemy.GetComponent<Enemy>();

        Assert.IsTrue(goblin.Health==5);
    }
    [Test]
    public void DoesEnemyHaveHealth()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy goblin = enemy.GetComponent<Enemy>();

        Assert.IsTrue(goblin.Health>0);
    }

    [UnityTest]
    public IEnumerator GoblinIsPatrolling()
    {
        yield return new WaitForSeconds(1f);
        GameObject enemyObject = GameObject.FindWithTag("Enemy");
        Rigidbody2D enemy = enemyObject.GetComponent<Rigidbody2D>();
        Assert.IsTrue(enemy.velocity.x != 0);
    }
}
