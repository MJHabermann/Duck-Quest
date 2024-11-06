using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private bool playerInRange = false; // Tracks if player is nearby to trigger dialogue
    private bool hasStartedDialogue = false; // Prevents multiple dialogue triggers

    public bool dialogueActive;

    // Start is called before the first frame update
    public void Start()
    {
        //dialogue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        if (playerInRange && !hasStartedDialogue)
        {
            StartDialogue();
            hasStartedDialogue = true; // Set this to true so dialogue starts only once
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player collided with NPC.");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            hasStartedDialogue = false; // Reset this flag when player leaves range
            dialogue.gameObject.SetActive(false); // Hide dialogue when player exits collision
            dialogueActive = false;
        }
    }

    public void StartDialogue()
    {
        dialogue.gameObject.SetActive(true);
        dialogueActive = true;
        dialogue.StartDialogue();
    }

}
*/

public class NPC : MonoBehaviour
{
    private INPCState currentState;

    public Dialogue dialogue;
    public bool dialogueActive;

    void Start()
    {
        TransitionToState(new NPCIdleState()); // Start in Idle state
    }

    public void Update()
    {
        currentState.UpdateState(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState.OnPlayerEnter(this);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState.OnPlayerExit(this);
        }
    }

    public void TransitionToState(INPCState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void StartDialogue()
    {
        dialogue.gameObject.SetActive(true);
        dialogueActive = true;
        dialogue.StartDialogue();
    }

    public void EndDialogue()
    {
        dialogue.gameObject.SetActive(false);
        dialogueActive = false;
    }
}
