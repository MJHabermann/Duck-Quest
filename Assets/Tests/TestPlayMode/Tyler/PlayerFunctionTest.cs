using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
public class PlayerTests
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
    public void PlayerMoveSpeedNotZero()
    {
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        Assert.IsTrue(player.moveSpeed != 0);
    }

    [Test]
    public void PlayerMementoSurvival()
    {
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        int bombs = player.getBombCount();
        player.OnRun();
        Assert.IsTrue(player.getBombCount() == bombs + 10);
        SceneManager.LoadScene("PlayerRoom", LoadSceneMode.Single);
        Assert.IsTrue(player.getBombCount() == bombs + 10);
        player.OnRun();
        Assert.IsTrue(player.getBombCount() == bombs + 20);
        SceneManager.LoadScene("Town", LoadSceneMode.Single);
        Assert.IsTrue(player.getBombCount() == bombs + 20);
    }

    [Test]
    public void PlayerCanIncreaseBombs()
    {
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        int bombs = player.getBombCount();
        player.OnRun();
        Assert.IsTrue(player.getBombCount() == bombs + 10);
    }

    [UnityTest]
    public IEnumerator PlayerPlacedBomb()
    {
        // wait for scene to call start()
        yield return new WaitForSeconds(1f);
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        player.OnBomb();
        GameObject b = GameObject.Find("Bomb(Clone)");
        // Assert player object exists
        Assert.IsTrue(b);
    }

    [Test]
    public void PlayerCanIncreaseArrows()
    {
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        int arrows = player.getArrowCount();
        player.OnRun();
        Assert.IsTrue(player.getArrowCount() == arrows + 10);
    }

        [UnityTest]
    public IEnumerator PlayerShotArrow()
    {
        // wait for scene to call start()
        yield return new WaitForSeconds(1f);
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        player.OnBow();
        GameObject a = GameObject.Find("Arrow(Clone)");
        // Assert player object exists
        Assert.IsTrue(a);
    }

    [Test]
    public void PlayerCanDie()
    {
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        DamageableCharacter pl = p.GetComponent<DamageableCharacter>();
        Assert.IsTrue(!player.isDead);
        pl.OnHit(999);
        Assert.IsTrue(player.isDead);
    }

    [UnityTest]
    public IEnumerator PlayerUsedHook()
    {
        // wait for scene to call start()
        yield return new WaitForSeconds(1f);
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        player.OnHook();
        GameObject b = GameObject.Find("Hook(Clone)");
        // Assert player object exists
        Assert.IsTrue(b);
    }

    [Test]
    public void PlayerHookReturn()
    {
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        player.isOccupied = true;
        Assert.IsTrue(player.isOccupied);
        player.HookReturn();
        Assert.IsTrue(!player.isOccupied);
    }

    [UnityTest]
    public IEnumerator PlayerMagicWillSpawn()
    {
        // wait for scene to call start()
        yield return new WaitForSeconds(1f);
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        player.MagicSword();
        GameObject b = GameObject.Find("Magic(Clone)");
        // Assert player object exists
        Assert.IsTrue(b);
    }

    [Test]
    public void PlayerDead()
    {
        GameObject p = GameObject.FindWithTag("Player");

        // Get the Player component attached to the player object
        Player player = p.GetComponent<Player>();
        DamageableCharacter pl = p.GetComponent<DamageableCharacter>();
        player.isDead = false;
        player.isOccupied = false;
        Assert.IsTrue(!player.isOccupied);
        Assert.IsTrue(!player.isDead);

        player.Dead();
        Assert.IsTrue(player.isOccupied); 
        Assert.IsTrue(player.isDead);
    }

    // [Test]
    // public void EnemyTypeIsGoblin(){
    //     // GameObject p = GameObject.FindWithTag("Player");

    //     Player player = FindObjectOfType<Player>();

    //     Assert.IsTrue(player.getBombCount()==0);
    // }
    
    // [Test]
    // public void doesGoblinExist()
    // {
    //     // Find the player object in the scene
    //     GameObject enemy = GameObject.FindWithTag("Enemy");

    //     // Assert that the enemy object exists
    //     Assert.IsNotNull(enemy);
    // }

    // [Test]
    // public void IsHealthSet()
    // {
    //     GameObject enemy = GameObject.FindWithTag("Enemy");

    //     // Get the Player component attached to the player object
    //     Enemy goblin = enemy.GetComponent<Enemy>();

    //     Assert.IsTrue(goblin.Health==5);
    // }
    // [Test]
    // public void DoesEnemyHaveHealth()
    // {
    //     GameObject enemy = GameObject.FindWithTag("Enemy");

    //     // Get the Player component attached to the player object
    //     Enemy goblin = enemy.GetComponent<Enemy>();

    //     Assert.IsTrue(goblin.Health>0);
    // }

    // [UnityTest]
    // public IEnumerator GoblinIsPatrolling()
    // {
    //     yield return new WaitForSeconds(1f);
    //     GameObject enemyObject = GameObject.FindWithTag("Enemy");
    //     Rigidbody2D enemy = enemyObject.GetComponent<Rigidbody2D>();
    //     Assert.IsTrue(enemy.velocity.x != 0);
    // }
}
*/
