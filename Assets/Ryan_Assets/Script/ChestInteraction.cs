/*
using UnityEngine;

public class ChestInteraction : MonoBehaviour {
    public PlayerHUD playerHUD;  // Reference to your PlayerHUD script
    public int moneyAmount = 1000;  // Amount of money to add when chest is opened
    private bool isPlayerNearby = false;  // Check if player is in range

    void Update() {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) {  // Check if player presses "E"
            CollectMoneyFromChest();
        }
    }

    void CollectMoneyFromChest() {
        playerHUD.AddMoney(moneyAmount);
        Debug.Log("Money added to player: " + moneyAmount);
        gameObject.SetActive(false);
    }

    // Trigger detection when player enters chest's collider
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {  // Make sure the player has the "Player" tag
            isPlayerNearby = true;
            Debug.Log("Player is near the chest.");
        }
    }

    // Trigger detection when player exits chest's collider
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isPlayerNearby = false;
            Debug.Log("Player left the chest's range.");
        }
    }
}
*/
