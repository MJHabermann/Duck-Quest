using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    public TMP_Text moneyText;
    public Image[] heartImages;

    public GameObject death;

    private int playerMoney;
    public int maxHealth;
    public int currentPlayerHealth;

    public float invincibilityDuration;

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

    public void UpdateMoneyUI()
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

    
    private IEnumerator Dead()
    {
        Debug.Log("Dead message recieved");
        death.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }

    private void Hit()
    {
        Debug.Log("Hit message recieved");
        currentPlayerHealth--;
        UpdateHealthUI();
    }
    
}