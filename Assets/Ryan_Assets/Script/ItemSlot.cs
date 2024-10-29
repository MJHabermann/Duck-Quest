using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot: MonoBehaviour, IPointerClickHandler
{
    //====ITEM DATA====///
    public string itemName;
    public int quantity;

    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberofItems;

    //Item Slot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    //ITEM DESCRIPTION//

    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory 1").GetComponent<InventoryManager>();
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //Check to see if slot is already full
        if(isFull)
            return quantity;

        //update name
        this.itemName = itemName;
        
        //update Image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        //update description
        this.itemDescription = itemDescription; 

        //update quantity
        this.quantity += quantity;
        if(this.quantity >= maxNumberofItems)
        {
            quantityText.text = maxNumberofItems.ToString();
            quantityText.enabled = true;
            isFull = true;
        

            //RETURN THE LEFTOVERS
            int extraItems = this.quantity - maxNumberofItems;
            this.quantity = maxNumberofItems;
            return extraItems;
        }
        //Update Quantity Text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite; 
        if(itemDescriptionImage.sprite ==null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }


    }

    public void OnRightClick()
    {

    }
}
