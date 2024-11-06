using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ChestInteraction : MonoBehaviour
{
    public PlayerHUD playerHUD;  // Reference to PlayerHUD script
    public InventoryManager playerInventory; // Reference to inventory manager script
    public int moneyAmount = 1000;  // Amount of money to add when chest is opened
    public bool isPlayerNearby = false;  // Check if player is in range
    public bool isLocked = true; // Check if the chest is locked
    public bool chestHasBeenOpened = false;

    private InputAction interactAction; // Input action for interacting with the chest

    private void Awake()
    {
        // Initialize the InputAction and bind it to "E" key (or another button if needed)
        interactAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/e");
        interactAction.performed += ctx => OnInteract();
    }

    private void OnEnable()
    {
        interactAction.Enable(); // Enable the action when the script is active
    }

    private void OnDisable()
    {
        interactAction.Disable(); // Disable the action when the script is inactive
    }

    private void OnInteract()
    {
        // Check if player is nearby and the chest has not been opened yet
        if (isPlayerNearby && !chestHasBeenOpened)
        {
            CollectMoneyFromChest();
        }
    }

    public void CollectMoneyFromChest()
    {
        Debug.Log("Collect Money from chest called");
        playerHUD.AddMoney(moneyAmount);
        Debug.Log("Money added to player: " + moneyAmount);
        chestHasBeenOpened = true;
        Debug.Log("Chest opened");
        //gameObject.SetActive(false); // Optional: disable chest after opening
    }

    // Trigger detection when player enters chest's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            isPlayerNearby = true;
            Debug.Log("Player is near the chest.");
        }
    }

    // Trigger detection when player exits chest's collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the chest's range.");
        }
    }
}
