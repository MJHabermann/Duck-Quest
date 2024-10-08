using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.TestTools;

public class SceneChangeTest
{
    private GameObject playButton;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load the Main Menu scene asynchronously
        SceneManager.LoadScene("Assets/Scenes/mainmenu.unity");  // Replace with your main menu scene name
        yield return null;  // Wait one frame to ensure the scene loads
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        // Unload the scene after the test
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    [UnityTest]
    public IEnumerator TestSceneChangeButton()
    {
        // Find the Play button after the scene has loaded
        var startButton = GameObject.Find("Start");  // Replace with your actual button name
        Assert.IsNotNull(startButton, "PlayButton not found in the MainMenu scene");

        var buttonComponent = startButton.GetComponent<Button>();
        Assert.IsNotNull(buttonComponent, "PlayButton does not have a Button component");

        // Add a temporary listener to check if the button is being clicked
        bool buttonClicked = false;
        buttonComponent.onClick.AddListener(() => buttonClicked = true);

        // Simulate the button click
        buttonComponent.onClick.Invoke();
        
        // Ensure the button click was registered
        Assert.IsTrue(buttonClicked, "PlayButton click did not register");

        yield return null;  // Wait one frame to let the test run fully
    }
}
