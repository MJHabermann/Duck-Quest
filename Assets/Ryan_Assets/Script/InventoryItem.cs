using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEngine;

public class InventoryItem: MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;
    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("Inventory 1").GetComponent<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found. Make sure there is a GameObject named 'Inventory' with the InventoryManager script attached.");
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player collided with " + itemName);
         int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
         if(leftOverItems <= 0)
         {
            Destroy(gameObject);
         }
         else
         {
            quantity = leftOverItems;
         }
        Destroy(gameObject);  // Remove the key after pickup
    }
}




}
