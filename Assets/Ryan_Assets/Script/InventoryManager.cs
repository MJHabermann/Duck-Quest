using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public bool menuActivated;
    [SerializeField] private Slider musicSlider;

    public ItemSlot[] itemSlot;
    public ItemSO[] itemSOs;

    private int totalItemCount;
    private bool isActive;

    private InputAction toggleInventoryAction; // Standalone action for Escape key

    private void Awake()
    {
        // Initialize the Escape key input action
        toggleInventoryAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        toggleInventoryAction.performed += ToggleInventory;
    }

    private void OnEnable()
    {
        // Enable the action when the script is active
        toggleInventoryAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the action when the script is not active
        toggleInventoryAction.Disable();
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        // Toggle the inventory menu
        menuActivated = !menuActivated;
        inventoryMenu.SetActive(menuActivated);
        Time.timeScale = menuActivated ? 0 : 1; // Pause/unpause the game
        Debug.Log(menuActivated ? "Inventory opened" : "Inventory closed");
    }

    public void changeVolume()
    {
        AudioListener.volume = musicSlider.value;
    }

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                return itemSOs[i].UseItem();
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        Debug.Log("Trying to add item: " + itemName + ", quantity: " + quantity);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                int itemsAdded = quantity - leftOverItems;
                UpdateItemCount(itemsAdded);

                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                }
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in itemSlot)
        {
            slot.selectedShader.SetActive(false);
            slot.thisItemSelected = false;
        }
    }

    private void UpdateItemCount(int change)
    {
        totalItemCount += change;
        Debug.Log("Total item count is now: " + totalItemCount);
    }

    public int GetItemCount()
    {
        Debug.Log("Total item count: " + totalItemCount);
        return totalItemCount;
    }
}
