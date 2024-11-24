using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private Coroutine typingCoroutine;

    private InputAction clickAction; // InputAction for UI click

    public InputActionAsset inputActions;

    public GameObject dialogBox;
    
    
    void Start()
    {
        textComponent.text = string.Empty;
    }

    void AdvanceDialogue()
    {
        if(clickAction.triggered)
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    private void Awake()
    {
        // Initialize the InputAction to detect a click
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Pointer>/press");
        clickAction.performed += ctx => AdvanceDialogue();
    }

    private void OnEnable()
    {
        // Enable the action when the script is active
        clickAction.Enable();
        
    }

    private void OnDisable()
    {
        // Disable the action when the script is not active
        clickAction.Disable();
    }

    public void StartDialogue()
    {
        dialogBox.SetActive(true);
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());

        inputActions.FindActionMap("Player")?.Disable();
    }


    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;

            if(typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android ||
               Application.platform == RuntimePlatform.IPhonePlayer;// ||
               //Application.isEditor; // Include Editor for testing with Unity Remote
    }
    public void EndDialogue()
    {
        dialogBox.SetActive(false);

        inputActions.FindActionMap("Player")?.Enable();
    }

}
