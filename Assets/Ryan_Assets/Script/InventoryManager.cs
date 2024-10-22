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

    public List<InventoryItem> items = new List<InventoryItem>();


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

    public void AddKey()
    {
        InventoryItem keyItem = items.Find(item => item.itemName == "Key");
        if(keyItem != null)
        {
            keyItem.quanity++;
        }
        else
        {
            items.Add(new InventoryItem("Key", 1));
        }
        Debug.Log("Key added to inventory");
    }

    public bool HasKey()
    {
        InventoryItem keyItem = items.Find(item => item.itemName == "Key");
        return keyItem != null && keyItem.quanity > 0;
    }

    public void UseKey()
    {
        InventoryItem keyItem = items.Find(item => item.itemName == "Key");
        if(keyItem != null && keyItem.quanity > 0)
        {
            keyItem.useItem();
            Debug.Log("Key used");
        }
        else
        {
            Debug.Log("No key available to be used.");
        }
    }
}
