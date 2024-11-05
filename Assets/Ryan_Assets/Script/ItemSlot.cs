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
    public int maxNumberofItems;

    //Item Slot
    [SerializeField]
    public TMP_Text quantityText;
    [SerializeField]
    public Image itemImage;

    //ITEM DESCRIPTION//

    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    public InventoryManager inventoryManager;

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
        if(thisItemSelected)
        {

            bool usable = inventoryManager.UseItem(itemName);
            if(usable)
            {
                this.quantity -= 1;
                isFull = false;
                quantityText.text = this.quantity.ToString();
                if(this.quantity <= 0)
                {
                    EmptySlot();
                }
            }
            
        }
        else
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
        



    }

    public void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite; 

    }

    public void OnRightClick()
    {
        /*
        //Create a new item
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        //Create and modify the SR
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 0;
        sr.sortingLayerName = "Ground";

        //Add a collider
        itemToDrop.AddComponent<BoxCollider2D>();

        //set the location
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(.5f, 0, 0);
        itemToDrop.transform.localScale = new Vector3(.5f, .5f, .5f);

        */

        //subtract item
        this.quantity -= 1;
        isFull = false;
        quantityText.text = this.quantity.ToString();
        if(this.quantity <= 0)
        {
            EmptySlot();
        }

    }
}
