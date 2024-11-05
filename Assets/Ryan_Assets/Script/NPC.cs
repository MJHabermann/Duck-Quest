using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private bool playerInRange = false; // Tracks if player is nearby to trigger dialogue
    private bool hasStartedDialogue = false; // Prevents multiple dialogue triggers

    // Start is called before the first frame update
    void Start()
    {
        dialogue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !hasStartedDialogue)
        {
            StartDialogue();
            hasStartedDialogue = true; // Set this to true so dialogue starts only once
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player collided with NPC.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            hasStartedDialogue = false; // Reset this flag when player leaves range
            dialogue.gameObject.SetActive(false); // Hide dialogue when player exits collision
        }
    }

    void StartDialogue()
    {
        dialogue.gameObject.SetActive(true);
        dialogue.StartDialogue();
    }
}
