using UnityEngine;

public class MobileManager : MonoBehaviour
{
    [SerializeField] private GameObject joystickUI; // Reference to your joystick UI GameObject
    [SerializeField] private GameObject otherInputUI; // Optional: Reference to other input UI (e.g., keyboard icons)

    private void Awake()
    {
        ActivateUIForPlatform();
    }

    private void ActivateUIForPlatform()
    {
        if (IsMobilePlatform())
        {
            Debug.Log("Running on a mobile platform. Activating joystick UI.");
            if (joystickUI != null)
                joystickUI.SetActive(true);
            if (otherInputUI != null)
                otherInputUI.SetActive(false); // Deactivate other input UI if applicable
        }
        else
        {
            Debug.Log("Running on a non-mobile platform. Deactivating joystick UI.");
            if (joystickUI != null)
                joystickUI.SetActive(false);
            if (otherInputUI != null)
                otherInputUI.SetActive(true); // Activate other input UI if applicable
        }
    }

    private bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android ||
               Application.platform == RuntimePlatform.IPhonePlayer; // ||
               //Application.isEditor; // Include Editor for testing with Unity Remote
    }
}