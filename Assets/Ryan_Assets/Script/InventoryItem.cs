using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string itemName;
    public int quanity;

    public InventoryItem(string name, int qty)
    {
        itemName = name;
        quanity = qty;
    }
    
    public void useItem()
    {
        if(quanity > 0)
        {
            quanity--;
        }
        else
        {
            //trigger dialogue box stating item cannot be used
        }
    }

}
