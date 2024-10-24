using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot: MonoBehaviour
{
    //====ITEM DATA====///
    public string itemName;
    public int quantity;

    public Sprite itemSprite;
    public bool isFull;

    //Item Slot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    // Start is called before the first frame update
    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;

        Debug.Log("Item added to slot: " + itemName + ", quantity: " + quantity + "itemsprite: " + itemSprite);
    }
}
