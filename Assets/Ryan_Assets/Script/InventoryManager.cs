using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Codice.CM.WorkspaceServer.Lock;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;

    public MonoBehaviour[] scriptsToToggle;
    [SerializeField] private Slider musicSlider;

    public ItemSlot[] itemSlot;




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

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log("Trying to add item: " + itemName + ", quantity: " + quantity);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite);
                Debug.Log("Item added to slot " + i);
                return;
            }
        }

        Debug.Log("No empty slots available for item: " + itemName);
    }

}
