using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vending machine

public class NPC : MonoBehaviour
{
    private INPCState currentState;

    public Dialogue dialogue;
    public bool dialogueActive;

    void Start()
    {
        TransitionToState(new NPCIdleState()); //begin in idle state, waiting for player
    }

    public void Update()
    {
        currentState.UpdateState(this); //update state when needed
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState.OnPlayerEnter(this); //go to engage state from idle state
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState.OnPlayerExit(this); // go to exit state from engage state
        }
    }

    public void TransitionToState(INPCState newState)
    {
        currentState = newState;
        currentState.EnterState(this); //change states
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
