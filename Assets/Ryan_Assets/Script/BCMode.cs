using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCMode : MonoBehaviour
{
    public Dialog dialogue; // Reference to your Dialog script

    public PlayerHUD hud;
    public IDamageable player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<IDamageable>();
            // Coroutine to make sure dialog and choice run one after the other
            StartCoroutine(HandleOrder());   
        }
    }

    private IEnumerator HandleOrder()
    {
        // Start dialogue
        dialogue.StartDialogue();
        
        // Wait for normal dialogue to end then start the choice
        yield return dialogue.WaitForDialogueToEndThenShowChoice();

            
            // Trigger the ShowChoice dialog
            dialogue.ShowChoice(
                "Do you seek the blessing of the Priestess?",
                () => AcceptBlessing(),   // Action for Yes
                () => DeclineBlessing()  // Action for No
            );
    }

    public void AcceptBlessing()
    {
        Debug.Log("The player has accepted the blessing.");
        player.OnHit(-10000);
        // hud.currentPlayerHealth = 10000;
        Debug.Log("current player health: " + player.Health);
    }

    public void DeclineBlessing()
    {
        Debug.Log("The player has declined the blessing.");
    }
}
