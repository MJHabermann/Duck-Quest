using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform handle; // Assign the handle (stick) in the inspector
    private Vector2 inputVector;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = handle.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = startPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out Vector2 pos);
        
        pos = Vector2.ClampMagnitude(pos, ((RectTransform)transform).sizeDelta.x / 2);
        handle.anchoredPosition = pos;

        inputVector = new Vector2(pos.x / ((RectTransform)transform).sizeDelta.x, 
                                  pos.y / ((RectTransform)transform).sizeDelta.y) * 2;
    }

    public Vector2 GetJoystickInput()
    {
        return inputVector;
    }

    // Public property to access the joystick's input vector
    public Vector2 Direction
    {
        get { return inputVector; }
    }
}
