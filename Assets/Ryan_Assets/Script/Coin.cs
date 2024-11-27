using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public PlayerHUD hud;

    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        hud.UpdateMoneyUI();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add money to the HUD
            hud.AddMoney(amount);

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
