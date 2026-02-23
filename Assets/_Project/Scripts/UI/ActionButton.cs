using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Image actionImage;

    private Action action;

    [HideInInspector] public UnityEvent<ActionButton> OnActionButtonClicked;

    public Action Action
    {
        get { return action; }
        set { action = value; actionImage.sprite = action.ActionIcon; }
    }

    public void OnClick()
    {
        OnActionButtonClicked?.Invoke(this);
    }
}
