using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCMode : MonoBehaviour
{
    public Dialog dialogue; // Reference to your Dialog script

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Coroutine to make sure dialog and choice run one after the other
            StartCoroutine(HandleOrder());   
        }
    }

    private IEnumerator HandleOrder()
    {
        // Start dialogue
        dialogue.StartDialogue();

        // Make choice box inactive to begin with
        dialogue.choiceBox.SetActive(false);
        
        // Wait for normal dialogue to end then start the choice
        yield return dialogue.WaitForDialogueToEndThenShowChoice();
            
            // Trigger the ShowChoice dialog
            dialogue.ShowChoice(
                "Do you seek the blessing of the Priestess?",
                () => AcceptBlessing(),   // Action for Yes
                () => DeclineBlessing()  // Action for No
            );
    }

    private void AcceptBlessing()
    {
        Debug.Log("The player has accepted the blessing.");
        // Add logic for accepting the blessing (e.g., boost stats, heal, etc.)
    }

    private void DeclineBlessing()
    {
        Debug.Log("The player has declined the blessing.");
        // Add logic for declining (e.g., neutral response, dialogue continues, etc.)
    }
}
