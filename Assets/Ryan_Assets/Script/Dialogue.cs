using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public int index;
    private Coroutine typingCoroutine;

    public void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                // Stop coroutine if it's still running to complete the line immediately
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        ShowLine();

    }

    public void ShowLine()
    {
        // Clear the text before typing to prevent duplication
        textComponent.text = string.Empty;

        // Only start the coroutine once per line
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Debug log to check if this coroutine starts multiple times
        Debug.Log("Starting typing for line: " + lines[index]);

        // Iterate over each character in the line
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            ShowLine();
        }
        else
        {
            gameObject.SetActive(false); // Close dialogue
        }
    }
}
