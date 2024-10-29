using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor.Build;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;

    public MonoBehaviour[] scriptsToToggle;
    [SerializeField] private Slider musicSlider;

    public ItemSlot[] itemSlot;

    public ItemSO[] itemSOs;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeVolume()
    {
        AudioListener.volume = musicSlider.value;

    }


    // Update is called once per frame
    void Update()
    {
        //unpaused
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
            Debug.Log("Game is resumed");
            EnableScripts();
        }
        //paused
        else if(Input.GetButtonDown("Inventory") && !menuActivated)
        {
            DisableScripts();
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
            Debug.Log("Game is paused");
        }
    }

     // Call this method from the Event Trigger to disable scripts
    public void DisableScripts() {
        foreach (MonoBehaviour script in scriptsToToggle) {
            script.enabled = false;
        }
    }

    // Call this method from the Event Trigger to enable scripts
    public void EnableScripts() {
        foreach (MonoBehaviour script in scriptsToToggle) {
            script.enabled = true;
        }
    }

    public void UseItem(string itemName)
    {
        for(int i = 0; i < itemSOs.Length; i++)
        {
            if(itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
            }
        }

    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        Debug.Log("Trying to add item: " + itemName + ", quantity: " + quantity);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                Debug.Log("Item added to slot " + i);
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
        for(int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

}
