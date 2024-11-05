using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossTests
{
    bool sceneLoaded;
    [OneTimeSetUp]
    public void LoadedLevel()
    {
        Debug.Log("Loading scene 'dungeon'...");
        SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        SceneManager.LoadScene("dungeon", LoadSceneMode.Single);
    }

    private void SceneManagerSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene 'dungeon' loaded.");
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
    public void BossExists()
    {
        // Find the player object in the scene
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Assert that the enemy object exists
        Assert.IsNotNull(enemy);
    }
    [Test]
    public void EnemyTypeIsBoss(){
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy boss = enemy.GetComponent<Enemy>();

        Assert.IsTrue(boss.EnemyName=="Boss");
    }
    [Test]
    public void IsHealthSet()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy goblin = enemy.GetComponent<Enemy>();

        Assert.IsTrue(goblin.Health==20);
    }
    [Test]
    public void DoesBossHaveHealth()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");

        // Get the Player component attached to the player object
        Enemy boss = enemy.GetComponent<Enemy>();

        Assert.IsTrue(boss.Health>0);
    }

}
