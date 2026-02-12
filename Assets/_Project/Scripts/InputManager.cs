using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    enum InputTypes { Mouse, Touchscreen }

    [SerializeField] private InputTypes inputType;

    [SerializeField] private UnityEvent<Vector2> OnRelease;

    private void Update()
    {
        switch (inputType)
        {
            case InputTypes.Mouse:
                UpdateMouseInput(); 
                break;
            case InputTypes.Touchscreen:
                UpdateTouchScreenInput();
                break;
            default:
                break;
        }
    }

    private void UpdateMouseInput()
    {
        if (Input.GetMouseButtonUp(0)) { 
            OnRelease?.Invoke(Input.mousePosition);
        }
    }

    private void UpdateTouchScreenInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) { 
            OnRelease?.Invoke(Input.GetTouch(0).position);
        }
    }
}
