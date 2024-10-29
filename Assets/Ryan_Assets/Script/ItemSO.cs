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

    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
           // GameObject.Find(" // GameObjectHealthmanager").GetComponent< // scriptPlayerHealth>().ChangeHealth(intAmountToChangeStat);
        }
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
