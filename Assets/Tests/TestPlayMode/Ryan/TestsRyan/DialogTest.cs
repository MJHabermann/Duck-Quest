using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;
/*

public class DialogTest
{
    private bool sceneLoaded = false;
    private const float loadTimeout = 5f;
    //private Dialogue dialogComponent;

    //private NPC npcComponent;

    private GameObject player;

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
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        float timer = 0f;
        while (!sceneLoaded && timer < loadTimeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Assert.IsTrue(sceneLoaded, "Scene 'Town' did not load within the expected time.");
        Assert.AreEqual("Town", SceneManager.GetActiveScene().name, "Failed to load the 'Town' scene as the active scene.");
        yield return null;

        // Find the parent Dialogue GameObject
        GameObject dialogueParentObject = GameObject.Find("Dialogue");
        Assert.IsNotNull(dialogueParentObject, "Parent Dialogue GameObject not found in the scene.");

        // Find the child DialogueBox GameObject
        Transform dialogueBoxTransform = dialogueParentObject.transform.Find("DialogueBox");
        Assert.IsNotNull(dialogueBoxTransform, "DialogueBox GameObject not found as a child of Dialogue.");

        // Get the Dialogue component from the DialogueBox GameObject
        dialogComponent = dialogueBoxTransform.GetComponent<Dialogue>();
        Assert.IsNotNull(dialogComponent, "Dialogue component not found on the DialogueBox GameObject.");

        npcComponent = GameObject.FindObjectOfType<NPC>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    /*
    [UnityTest]
    public IEnumerator DialogueStaysOpenAfterLastLine()
    {
        dialogComponent.StartDialogue();
        for (int i = 0; i < dialogComponent.lines.Length; i++)
        {
            yield return new WaitForSeconds(dialogComponent.textSpeed * dialogComponent.lines[i].Length + 0.1f);
            dialogComponent.Ha(); // Simulate click to advance
        }

        Assert.IsTrue(dialogComponent.gameObject.activeSelf, "Dialogue did not close after the last line.");
    }
*/
/*
    [UnityTest]
    public IEnumerator TextTypingSpeedRespectsSetting()
    {
        dialogComponent.textSpeed = 0.05f;
        dialogComponent.StartDialogue();

        float expectedDuration = dialogComponent.textSpeed * dialogComponent.lines[0].Length;
        yield return new WaitForSeconds(expectedDuration + 0.1f);

        Assert.AreEqual(dialogComponent.lines[0], dialogComponent.textComponent.text, "Text did not type at the expected speed.");
    }

    [UnityTest]
    public IEnumerator DialogueStartsWhenPlayerEntersRange()
    {
        // Simulate player entering the NPC's range
        npcComponent.OnTriggerEnter2D(player.GetComponent<Collider2D>());

        // Wait a frame to allow for updates
        yield return null;

        // Check that dialogue is active
        Assert.IsTrue(npcComponent.dialogue.gameObject.activeSelf, "Dialogue should be active when the player enters range.");
        Assert.IsTrue(npcComponent.dialogueActive, "NPC dialogueActive flag should be true when dialogue is active.");
    }

    [UnityTest]
    public IEnumerator DialogueDoesNotStartAgainWhenPlayerStaysInRange()
    {
        // Simulate player entering the NPC's range
        npcComponent.OnTriggerEnter2D(player.GetComponent<Collider2D>());
        
        // Wait a frame to ensure dialogue has started
        yield return null;

        // Check dialogue is active after initial trigger
        Assert.IsTrue(npcComponent.dialogue.gameObject.activeSelf, "Dialogue should be active after initial trigger.");
        
        // Try starting dialogue again to simulate staying in range
        npcComponent.Update();  // Calls Update, which would trigger dialogue if not guarded

        // Ensure the dialogue has not restarted (dialogue should be already active)
        Assert.IsTrue(npcComponent.dialogue.gameObject.activeSelf, "Dialogue should remain active without restarting when player stays in range.");
    }

    [UnityTest]
    public IEnumerator DialogueClosesWhenPlayerLeavesRange()
    {
        // Simulate player entering and then leaving the NPC's range
        npcComponent.OnTriggerEnter2D(player.GetComponent<Collider2D>());
        yield return null; // Wait a frame for activation
        
        npcComponent.OnTriggerExit2D(player.GetComponent<Collider2D>());
        yield return null; // Wait a frame for deactivation

        // Check that dialogue is inactive after player leaves range
        Assert.IsFalse(npcComponent.dialogue.gameObject.activeSelf, "Dialogue should be inactive when the player leaves range.");
        Assert.IsFalse(npcComponent.dialogueActive, "NPC dialogueActive flag should be false when dialogue is inactive.");
    }

    [UnityTest]
    public IEnumerator DialogueResetsWhenPlayerReentersRange()
    {
        // Simulate player entering, leaving, and re-entering the NPC's range
        npcComponent.OnTriggerEnter2D(player.GetComponent<Collider2D>());
        yield return null; // Wait for dialogue to start

        npcComponent.OnTriggerExit2D(player.GetComponent<Collider2D>());
        yield return null; // Wait for dialogue to close

        // Re-enter the range to restart the dialogue
        npcComponent.OnTriggerEnter2D(player.GetComponent<Collider2D>());
        yield return null; // Wait for dialogue to start again

        // Verify dialogue is active upon re-entering
        Assert.IsTrue(npcComponent.dialogue.gameObject.activeSelf, "Dialogue should be active when player reenters range.");
        Assert.IsTrue(npcComponent.dialogueActive, "NPC dialogueActive flag should be true when player reenters range.");
    }
}
*/