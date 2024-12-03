using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int intAmountToChangeStat;
    public StatToChange statToChange = new StatToChange();


    public bool UseItem()
    {
        
        if (statToChange == StatToChange.health)
        {
            PlayerHUD playerHealth = GameObject.FindObjectOfType<PlayerHUD>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHUD script not found in the scene.");
                return false;
            }

            if (playerHealth.currentPlayerHealth == playerHealth.maxHealth)
            {
                Debug.Log("Player already at max health.");
                return false; // Item not used
            }

            playerHealth.Heal(); // Call the Heal method
            Debug.Log($"{itemName} used to heal the player.");
            return true; // Item successfully used
        }

        if(statToChange == StatToChange.key)
        {
            //key logic
        }

        return false; // Item not used
        
        
    }
    

    public enum StatToChange
    {
        none,
        health,

        key
    };

}
