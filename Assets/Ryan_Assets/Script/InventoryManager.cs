using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;

    public GameObject joystick;

    public GameObject hud;
    public bool menuActivated;
    [SerializeField] private Slider musicSlider;

    public ItemSlot[] itemSlot;
    public ItemSO[] itemSOs;

    private int totalItemCount;
    private bool isActive;

    public InputActionAsset inputActions; // Reference to the Input Action Asset
    private InputActionMap playerActionMap;
    private InputActionMap uiActionMap;
    private InputAction toggleInventoryAction; // Standalone action for Escape key

    private void Start()
{
    // Dynamically find HUD in case it's lost during scene transitions
    hud = GameObject.Find("UI/PlayerHUD");
    if (hud == null)
    {
        Debug.LogError("HUD not found in the current scene.");
    }
    else
    {
        Debug.Log("HUD successfully reassigned.");
    }
}
    private void Awake()
    {
        // Get references to action maps
        playerActionMap = inputActions.FindActionMap("Player");
        uiActionMap = inputActions.FindActionMap("UI");

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

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        Debug.Log("Toggle inventory called");
        // Toggle the inventory menu
        menuActivated = !menuActivated;
        inventoryMenu.SetActive(menuActivated);
        joystick.SetActive(!menuActivated);
        hud.SetActive(!menuActivated);
        Time.timeScale = menuActivated ? 0 : 1; // Pause/unpause the game
        if (menuActivated)
        {
            Debug.Log("Player actions disabled");
            DisablePlayerActions();
        }
        else
        {
            Debug.Log("Player Actions Enabled");
            EnablePlayerActions();
        }
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

    private void EnablePlayerActions()
    {
        playerActionMap.Enable();
    }

    private void DisablePlayerActions()
    {
        playerActionMap.Disable();
    }
}

