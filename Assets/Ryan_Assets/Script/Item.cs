using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public string itemDescription;

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
        
        Player player = other.GetComponent<Player>();
        if(itemName == "Key"){
            player.setKeyCount(1);
        }else if(itemName == "Arrow"){
            player.increaseArrowCount(10);
        }else if(itemName == "Bomb"){
            player.increaseBombCount(5);
        }

        if (leftOverItems <= 0)
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
