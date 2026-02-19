using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    enum InputTypes { Mouse, Touchscreen }

    [SerializeField] private InputTypes inputType;

    [SerializeField] private UnityEvent<Vector2> OnCursorDown;
    [SerializeField] private UnityEvent<Vector2> OnCursorUp;

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
        if (PointerOverUI()) return;

        OnCursorDown?.Invoke(inputPosition);
    }

    private void OnInputUp(Vector2 inputPosition) 
    {
        if (PointerOverUI()) return;

        OnCursorUp?.Invoke(inputPosition);
    }

    private bool PointerOverUI()
    {
        switch (inputType)
        {
            case InputTypes.Mouse:
                return EventSystem.current.IsPointerOverGameObject();
            case InputTypes.Touchscreen:
                return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            default:
                return false;
        }
    }
}
