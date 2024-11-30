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
    
    // Yes/No choice elements
    public GameObject choiceBox;          // Panel for Yes/No buttons
    public TextMeshProUGUI choiceText;    // Text above the buttons
    public GameObject yesButton;          // Yes button
    public GameObject noButton;           // No button

    private System.Action onYes;          // Action for Yes
    private System.Action onNo;           // Action for No

    private bool dialogueFinished = false;        // Flag for BC Mode script
    
    void Start()
    {
        textComponent.text = string.Empty;
        if (choiceBox != null) choiceBox.SetActive(false);
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
        if (textComponent.gameObject != null)
        {
            textComponent.gameObject.SetActive(true);
            textComponent.text = string.Empty;
        }
        if (choiceBox != null) choiceBox.SetActive(false);
        index = 0;
        dialogueFinished = false;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());

        inputActions.FindActionMap("Player")?.Disable();
    }


    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            textComponent.ForceMeshUpdate();
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

        dialogueFinished = true;

        inputActions.FindActionMap("Player")?.Enable();
    }

    public void ShowChoice(string message, System.Action yesAction, System.Action noAction)
    {
        inputActions.FindActionMap("Player")?.Disable();

        choiceBox.SetActive(true);

        choiceText.text = message;
        onYes = yesAction;
        onNo = noAction;

        // Add listeners to Yes and No buttons
        yesButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        yesButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            onYes?.Invoke();
            Debug.Log("Player chose Yes");
            CloseChoice();
        });

        noButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        noButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            onNo?.Invoke();
            Debug.Log("Player chose no");
            CloseChoice();
        });
    }

    private void CloseChoice()
    {
        choiceBox.SetActive(false);
        inputActions.FindActionMap("Player")?.Enable();
    }

    public IEnumerator WaitForDialogueToEndThenShowChoice()
    {
        // Wait until the dialogue is finished
        yield return new WaitUntil(() => dialogueFinished);

        
    }


}
