using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    enum InputTypes { Mouse, Touchscreen }

    [SerializeField] private InputTypes inputType;
    [SerializeField] private float maxInputDragDistance = 10;

    [SerializeField] private UnityEvent<Vector2> OnCursorDown;
    [SerializeField] private UnityEvent<Vector2> OnCursorUp;

    private Vector2 lastSelectPosition;

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
        if (Input.GetMouseButtonDown(0)) { 
            OnInputDown(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) { 
            OnInputUp(Input.mousePosition);
        }
    }

    private void UpdateTouchScreenInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase){
            case TouchPhase.Began:
                OnInputDown(touch.position);
                break;
            case TouchPhase.Ended:
                OnInputUp(touch.position);
                break;
            default:
                break;

        }
    }

    private void OnInputDown(Vector2 inputPosition)
    {
        lastSelectPosition = inputPosition;
        OnCursorDown?.Invoke(lastSelectPosition);
    }

    private void OnInputUp(Vector2 inputPosition) 
    {
        if ((inputPosition - lastSelectPosition).sqrMagnitude > Mathf.Pow(maxInputDragDistance, 2)) return;

        OnCursorUp?.Invoke(lastSelectPosition);
    }
}
