using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Serialization;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int intAmountToChangeStat;
    public StatToChange statToChange = new StatToChange();
    public AttributesToChange attributesToChange = new AttributesToChange();
    public int intAmountToChangeAttribute;


    public bool UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            //PlayerHealth playerHealth = // GameObject.Find(" // GameObjectHealthmanager").GetComponent< // scriptPlayerHealth>().ChangeHealth(intAmountToChangeStat);
            //if playerHealth.health == playerHealth.maxHealth)
            //return false
            //else
           //playerHealth.RestoreHealth(amountToChangeStat);
           //return true

           //return false;
        }
        return true;

    }
    

    public enum StatToChange
    {
        none,
        health
    };

    public enum AttributesToChange
    {
        none,
        heatlh
    };
}
