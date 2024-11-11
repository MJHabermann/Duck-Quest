using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    public TMP_Text moneyText;  // Assign in Inspector
    public Image[] heartImages;  // Assign in Inspector

    private int playerMoney;
    private int maxHealth;
    private int currentPlayerHealth;

    public float invincibilityDuration;

    private bool isInvincible = false;

    void Start()
    {
        // Debugging for missing assignments
        if (moneyText == null)
        {
            Debug.LogError("Money Text is not assigned in the Inspector!");
        }

        if (heartImages == null || heartImages.Length == 0)
        {
            Debug.LogError("Heart images not set in the Inspector!");
        }

        // Initialize health and money
        maxHealth = 3;
        currentPlayerHealth = 3;
        playerMoney = 0;

        // Update the UI at the start
        UpdateHealthUI();
        UpdateMoneyUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        if (currentPlayerHealth > 0)
        {
            currentPlayerHealth--;
            UpdateHealthUI();
            StartCoroutine(BecomeInvincible());
        }
        else if(currentPlayerHealth <= 0)
        {
            StopAllCoroutines();
            PlayerDies();
        }
    }
    
    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        Debug.Log("Player currently invincible");
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        Debug.Log("Player is no longer invincible.");
    }

    void PlayerDies()
    {
        Debug.Log("Player is dead.");
    }
    public void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentPlayerHealth)
            {
                heartImages[i].gameObject.SetActive(true);
            }
            else
            {
                heartImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void Heal()
    {
        currentPlayerHealth = maxHealth;
        UpdateHealthUI();
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log("Money increased by " + amount + ". Total money: " + playerMoney);
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = playerMoney.ToString();
        }
        else
        {
            Debug.LogError("Money Text is missing! Cannot update UI.");
        }
    }
    public int GetMoney()
    {
        return playerMoney;
    }

    
    private void Dead()
    {
        Debug.Log("Dead message recieved");
        SceneManager.LoadScene("GameOver");
    }
    
}