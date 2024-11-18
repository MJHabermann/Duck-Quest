using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private Coroutine typingCoroutine;
    private bool canAdvance = true; // Flag to allow advancing

    private InputAction clickAction; // InputAction for UI click

    public InputActionAsset inputActions;
    
    

    private void Awake()
    {
        // Initialize the InputAction to detect a click
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Pointer>/press");
        clickAction.performed += OnAdvanceDialogue;
    }

    private void OnEnable()
    {
        // Enable the action when the script is active
        clickAction.Enable();

        inputActions.FindActionMap("Player")?.Disable();
        inputActions.FindActionMap("UI")?.Enable();
    }

    private void OnDisable()
    {
        // Disable the action when the script is not active
        clickAction.Disable();
        inputActions.FindActionMap("Player")?.Enable();
        inputActions.FindActionMap("UI")?.Disable();
    }

    private void OnAdvanceDialogue(InputAction.CallbackContext context)
    {
        if (canAdvance)
        {
            AdvanceDialogue();
            canAdvance = false; // Temporarily block advancement until the line is complete
            StartCoroutine(ResetCanAdvance());
        }
    }

    private IEnumerator ResetCanAdvance()
    {
        yield return new WaitForSeconds(0.2f); // Short delay to prevent rapid input
        canAdvance = true;
    }

    public void StartDialogue()
    {
        index = 0;
        ShowLine();
    }

    private void ShowLine()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        textComponent.text = string.Empty;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeLine());
    }


    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        canAdvance = true;
    }

    private void AdvanceDialogue()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            textComponent.text = lines[index];
            canAdvance = true;
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        //gameObject.SetActive(false); // Close the dialogue UI
        textComponent.text = string.Empty;
        textComponent.transform.parent.gameObject.SetActive(false); // Assumes TextMeshPro is a child of the dialogue box
    }

}
