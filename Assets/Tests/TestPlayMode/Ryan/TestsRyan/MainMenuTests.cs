using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.UI;
using System.Collections;

public class MainMenuTests
{
    private GameObject mainMenuCanvas;
    private Button startButton;
    private Button quitButton;
    private AudioSource mainThemeSong;
    private AudioSource sceneChangeSFX;

    [SetUp]
    public void Setup()
    {
        // Assuming mainMenuCanvas contains the main menu and buttons are set up
        mainMenuCanvas = GameObject.Find("Canvas");
        startButton = GameObject.Find("Canvas/StartButton").GetComponent<Button>();
        quitButton = GameObject.Find("Canvas/QuitButton").GetComponent<Button>();
        mainThemeSong = GameObject.Find("Main Theme Song").GetComponent<AudioSource>();
        sceneChangeSFX = GameObject.Find("Scene Change SFX").GetComponent<AudioSource>();
    }

    [UnityTest]
    public IEnumerator StartButtonLoadsGameScene()
    {
        // Verify the button click loads the next scene
        startButton.onClick.Invoke();

        // Wait for a frame to let the scene load
        yield return new WaitForSeconds(1f);

        // Check that the new scene has loaded (replace "GameScene" with your actual scene name)
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }

    [Test]
    public void QuitButtonExists()
    {
        // Check that the Quit button is not null (exists in the scene)
        Assert.NotNull(quitButton, "Quit button is missing from the main menu.");
    }

    [UnityTest]
    public IEnumerator MainThemeSongPlaysOnMenuLoad()
    {
        // Ensure the main theme song is playing on menu load
        Assert.IsTrue(mainThemeSong.isPlaying, "Main theme song is not playing on main menu load.");

        // Wait a frame for loop verification
        yield return null;

        // Verify the song is looping correctly
        Assert.IsTrue(mainThemeSong.loop, "Main theme song is not set to loop.");
    }

    [UnityTest]
    public IEnumerator SceneChangeSoundPlaysOnStart()
    {
        // Click the Start button to trigger the scene change
        startButton.onClick.Invoke();

        // Wait a short moment to detect the SFX
        yield return new WaitForSeconds(0.1f);

        // Verify the scene change sound effect is playing
        Assert.IsTrue(sceneChangeSFX.isPlaying, "Scene change sound effect did not play.");
    }

    [UnityTest]
    public IEnumerator ButtonsPlayClickSound()
    {
        // Simulate clicking the start button and verify sound
        startButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        // Assuming AudioSource Button SFX is attached to buttons, replace with actual AudioSource component if different
        var audioSourceButton = GameObject.Find("Audio Source Button").GetComponent<AudioSource>();
        Assert.IsTrue(audioSourceButton.isPlaying, "Button click sound did not play on start button click.");
        
        // Repeat for quit button if desired
        quitButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(audioSourceButton.isPlaying, "Button click sound did not play on quit button click.");
    }
}
