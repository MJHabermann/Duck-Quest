using UnityEngine;

public class ChestInteraction : MonoBehaviour {
    public PlayerHUD playerHUD;  // Reference to your PlayerHUD script

    public InventoryManager playerInventory; //reference to inventory manager script
    private int moneyAmount = 1000;  // Amount of money to add when chest is opened
    private bool isPlayerNearby = false;  // Check if player is in range

    private bool isLocked = true; //check if the chest is locked

    private bool chestHasBeenOpened = false;

    void Update() {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !chestHasBeenOpened) {  // Check if player presses "E"
            if(isLocked)
            {
                TryUnlockChest();
            }
            else
            {
                CollectMoneyFromChest();
            }
            
        }
    }

    void TryUnlockChest()
    {
        if (playerInventory.HasKey())
        {
            playerInventory.UseKey();
            isLocked = false;
            Debug.Log("Chest unlocked");
            CollectMoneyFromChest();
        }
        else
        {
            //you need a key to unlock the chest
        }
    }
    void CollectMoneyFromChest() 
    {
        Debug.Log("Collect Money from chest called");
        playerHUD.AddMoney(moneyAmount);
        Debug.Log("Money added to player: " + moneyAmount);
        chestHasBeenOpened = true;
        Debug.Log("Chest opened");
        //gameObject.SetActive(false);
    }

    // Trigger detection when player enters chest's collider
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {  // Make sure the player has the "Player" tag
            isPlayerNearby = true;
            Debug.Log("Player is near the chest.");
        }
    }

    // Trigger detection when player exits chest's collider
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isPlayerNearby = false;
            Debug.Log("Player left the chest's range.");
        }
    }
}

