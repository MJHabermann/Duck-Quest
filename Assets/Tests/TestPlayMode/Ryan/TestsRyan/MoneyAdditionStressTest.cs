using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MoneyAdditionStressTest
{
    public PlayerHUD playerHUD;  // Assign your PlayerHUD instance in the Inspector

    private int initialMoney;
    private int addAmount = 100; // Amount of money to add each time
    private int iterations = 1000; // Number of times money will be added to stress test

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Ensure playerHUD is assigned
        Assert.IsNotNull(playerHUD, "PlayerHUD instance is not assigned in the Inspector.");
        
        initialMoney = playerHUD.GetMoney(); // Record initial balance
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestRapidMoneyAddition()
    {
        // Perform rapid additions of money
        for (int i = 0; i < iterations; i++)
        {
            playerHUD.AddMoney(addAmount);
            yield return null; // Yield for a frame to simulate time passing
        }

        // Calculate expected final balance
        int expectedMoney = initialMoney + (addAmount * iterations);
        int finalMoney = playerHUD.GetMoney();
        
        // Verify if the final balance is correct
        Assert.AreEqual(expectedMoney, finalMoney, $"Final money amount does not match expected value after {iterations} additions.");

        Debug.Log($"Stress Test Complete: Expected Money = {expectedMoney}, Final Money = {finalMoney}");
    }
}
